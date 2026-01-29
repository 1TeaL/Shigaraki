using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.UI;
using ShiggyMod.Modules.Networking;
using ShiggyMod.Modules.Quirks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ShiggyMod.Modules.UI
{
    public class GiveQuirkUI : MonoBehaviour
    {
        // ---------- Singleton ----------
        public static GiveQuirkUI Current { get; private set; }
        public static bool IsOpen => Current != null;

        public static GiveQuirkUI Show(CharacterBody giver, CharacterBody allyTarget, Action<string, float> toast = null)
        {
            if (!giver || !allyTarget) return null;

            // Only the local controlling client should open this UI
            if (!giver.hasEffectiveAuthority) return null;

            if (Current) return Current;

            var root = new GameObject("Shiggy_GiveQuirkUI_Root");
            DontDestroyOnLoad(root);

            Current = root.AddComponent<GiveQuirkUI>();
            Current._giver = giver;
            Current._target = allyTarget;
            Current._toast = toast;
            Current.BuildUI();
            return Current;
        }

        // ---------- Internals ----------
        private CharacterBody _giver;
        private CharacterBody _target;
        private Action<string, float> _toast;

        private Canvas _canvas;

        private ScrollRect _scroll;
        private RectTransform _content;

        private Button _confirmBtn, _cancelBtn;
        private QuirkId _selected = QuirkId.None;

        private List<QuirkId> _ownedPassives = new List<QuirkId>();
        private readonly Dictionary<QuirkId, Button> _rowButtons = new Dictionary<QuirkId, Button>();

        private bool _subscribed;

        // NEW: instance inventory (giver's master-scoped)
        private QuirkInventory _giverInv;

        // ---------- Build ----------
        private void BuildUI()
        {
            // Resolve giver inventory FIRST (so list-building works)
            ResolveGiverInventory();

            // Canvas
            var canvasGO = new GameObject("Canvas");
            canvasGO.transform.SetParent(transform, false);
            _canvas = canvasGO.AddComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var scaler = canvasGO.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);

            canvasGO.AddComponent<GraphicRaycaster>();

            // Dimmer (click outside to close)
            var dim = NewUI<Image>("Dim", canvasGO.transform);
            dim.color = new Color(0f, 0f, 0f, 0.65f);
            StretchFull(dim.rectTransform);
            var dimBtn = dim.gameObject.AddComponent<Button>();
            dimBtn.onClick.AddListener(Close);

            // Panel
            var panel = NewUI<Image>("Panel", canvasGO.transform);
            panel.color = new Color(0.08f, 0.08f, 0.08f, 0.95f);
            var prt = panel.rectTransform;
            prt.sizeDelta = new Vector2(760f, 560f);
            prt.anchorMin = prt.anchorMax = new Vector2(0.5f, 0.5f);
            prt.anchoredPosition = Vector2.zero;

            // Title
            var title = NewText("Title", panel.transform, "Give Passive Quirk");
            title.alignment = TextAnchor.MiddleCenter;
            title.fontSize = 28;
            var trt = title.rectTransform;
            trt.anchorMin = new Vector2(0f, 1f);
            trt.anchorMax = new Vector2(1f, 1f);
            trt.pivot = new Vector2(0.5f, 1f);
            trt.sizeDelta = new Vector2(0f, 46f);
            trt.anchoredPosition = new Vector2(0f, -10f);

            // Target subtitle
            var sub = NewText("Subtitle", panel.transform, $"Target: <color=#8f8>{SafeUserName(_target)}</color>");
            sub.alignment = TextAnchor.MiddleCenter;
            var srt = sub.rectTransform;
            srt.anchorMin = new Vector2(0f, 1f);
            srt.anchorMax = new Vector2(1f, 1f);
            srt.pivot = new Vector2(0.5f, 1f);
            srt.sizeDelta = new Vector2(0f, 28f);
            srt.anchoredPosition = new Vector2(0f, -46f);

            // ===== ScrollRect (correct hierarchy) =====
            var scrollGO = new GameObject("ScrollRect");
            scrollGO.transform.SetParent(panel.transform, false);

            var scrollRT = scrollGO.AddComponent<RectTransform>();
            scrollRT.anchorMin = new Vector2(0f, 0f);
            scrollRT.anchorMax = new Vector2(1f, 1f);
            scrollRT.offsetMin = new Vector2(10f, 90f);
            scrollRT.offsetMax = new Vector2(-10f, -70f);

            var scrollImg = scrollGO.AddComponent<Image>();
            scrollImg.color = new Color(0f, 0f, 0f, 0f);

            _scroll = scrollGO.AddComponent<ScrollRect>();
            _scroll.horizontal = false;
            _scroll.vertical = true;

            var viewportGO = new GameObject("Viewport");
            viewportGO.transform.SetParent(scrollGO.transform, false);
            var viewportRT = viewportGO.AddComponent<RectTransform>();
            viewportGO.AddComponent<RectMask2D>();
            var vimg = viewportGO.AddComponent<Image>();
            vimg.color = new Color(0f, 0f, 0f, 0.2f);
            viewportRT.anchorMin = Vector2.zero;
            viewportRT.anchorMax = Vector2.one;
            viewportRT.offsetMin = Vector2.zero;
            viewportRT.offsetMax = Vector2.zero;

            _scroll.viewport = viewportRT;

            var contentGO = new GameObject("Content");
            contentGO.transform.SetParent(viewportGO.transform, false);
            _content = contentGO.AddComponent<RectTransform>();
            _content.anchorMin = new Vector2(0f, 1f);
            _content.anchorMax = new Vector2(1f, 1f);
            _content.pivot = new Vector2(0.5f, 1f);

            _scroll.content = _content;

            // Buttons row
            var btnRow = NewUI<HorizontalLayoutGroup>("Buttons", panel.transform);
            btnRow.childAlignment = TextAnchor.MiddleRight;
            btnRow.spacing = 8f;
            var btnRowRT = (RectTransform)btnRow.transform;
            btnRowRT.anchorMin = new Vector2(0f, 0f);
            btnRowRT.anchorMax = new Vector2(1f, 0f);
            btnRowRT.pivot = new Vector2(1f, 0f);
            btnRowRT.sizeDelta = new Vector2(-22f, 40f);
            btnRowRT.anchoredPosition = new Vector2(-11f, 12f);

            _confirmBtn = BuildButton(btnRowRT, "Confirm", OnConfirm);
            _cancelBtn = BuildButton(btnRowRT, "Cancel", Close);

            // Build list & select first
            RebuildOwnedPassiveList();
            BuildRows();
            SelectFirstIfAny();

            UICursorUtil.OpenGameCursor();

            // subscribe after UI is up
            TrySubscribe();
        }
        private void OnEnable() => TrySubscribe();
        private void TrySubscribe()
        {
            ResolveGiverInventory();

            if (!_subscribed && _giverInv != null)
            {
                _giverInv.OnOwnedChanged += HandleOwnedChanged;
                _subscribed = true;
            }
        }
        private void ResolveGiverInventory()
        {
            if (_giverInv != null) return;
            if (!_giver || !_giver.master) return;

            _giverInv = QuirkInventory.Ensure(_giver.master);
        }

        private static string SafeUserName(CharacterBody body)
        {
            try { return body ? body.GetUserName() : "(None)"; }
            catch { return body ? body.name : "(None)"; }
        }

        private void RebuildOwnedPassiveList()
        {
            ResolveGiverInventory();

            if (_giverInv == null)
            {
                _ownedPassives = new List<QuirkId>();
                return;
            }

            _ownedPassives = _giverInv.Owned
                .Where(id =>
                    QuirkRegistry.TryGet(id, out var rec) &&
                    rec.Category == QuirkCategory.Passive &&
                    rec.BuffDef != null)
                .OrderBy(id => QuirkRegistry.Get(id).Level)
                .ThenBy(id => QuirkInventory.QuirkPickupUI.MakeNiceName(id))
                .ToList();
        }

        private void BuildRows()
        {
            foreach (Transform t in _content) Destroy(t.gameObject);
            _rowButtons.Clear();

            float y = -6f;
            foreach (var q in _ownedPassives)
            {
                var row = NewUI<Image>($"Row_{q}", _content);
                row.color = new Color(1f, 1f, 1f, 0.06f);
                var rrt = row.rectTransform;
                rrt.anchorMin = new Vector2(0f, 1f);
                rrt.anchorMax = new Vector2(1f, 1f);
                rrt.pivot = new Vector2(0.5f, 1f);
                rrt.sizeDelta = new Vector2(0f, 42f);
                rrt.anchoredPosition = new Vector2(0f, y);
                y -= 44f;

                var btn = row.gameObject.AddComponent<Button>();
                btn.targetGraphic = row;

                var icon = QuirkRegistry.GetIcon(q);
                if (icon != null)
                {
                    var ic = NewUI<Image>("Icon", row.transform);
                    ic.sprite = icon;
                    var irt = ic.rectTransform;
                    irt.anchorMin = new Vector2(0f, 0f);
                    irt.anchorMax = new Vector2(0f, 1f);
                    irt.pivot = new Vector2(0f, 0.5f);
                    irt.sizeDelta = new Vector2(40f, 0f);
                    irt.anchoredPosition = new Vector2(4f, 0f);
                }

                var label = NewText("Label", row.transform, QuirkInventory.QuirkPickupUI.MakeNiceName(q));
                label.alignment = TextAnchor.MiddleLeft;
                var lrt = label.rectTransform;
                StretchFull(lrt);
                lrt.offsetMin = new Vector2(50f, 0f);

                btn.onClick.AddListener(() => Select(q));
                _rowButtons[q] = btn;
            }

            _content.sizeDelta = new Vector2(0f, Mathf.Max(0f, -y + 6f));
        }

        private void SelectFirstIfAny()
        {
            if (_ownedPassives.Count > 0) Select(_ownedPassives[0]);
        }

        private void Select(QuirkId id)
        {
            _selected = id;

            foreach (var kv in _rowButtons)
            {
                var img = kv.Value.targetGraphic as Image;
                if (!img) continue;

                img.color = kv.Key.Equals(id)
                    ? new Color(1f, 1f, 1f, 0.18f)
                    : new Color(1f, 1f, 1f, 0.06f);
            }
        }

        private bool LocalValidateRangeAndTeam(float maxDistSqr = 30f * 30f)
        {
            if (!_giver || !_target) return false;

            var gTeam = _giver.teamComponent ? _giver.teamComponent.teamIndex : TeamIndex.None;
            var tTeam = _target.teamComponent ? _target.teamComponent.teamIndex : TeamIndex.None;
            if (gTeam != tTeam) return false;

            return (_target.corePosition - _giver.corePosition).sqrMagnitude <= maxDistSqr;
        }

        private void OnConfirm()
        {
            if (_selected == QuirkId.None)
            {
                _toast?.Invoke("<style=cIsDamage>No passive selected.</style>", 1.6f);
                return;
            }

            if (!_giver || !_target || !_giver.master || !_target.master)
            {
                _toast?.Invoke("<style=cDeath>Missing giver/target master.</style>", 1.8f);
                Close();
                return;
            }

            if (!LocalValidateRangeAndTeam())
            {
                _toast?.Invoke("<style=cIsDamage>Out of range or not an ally.</style>", 1.6f);
                return;
            }

            new GivePassiveRequest(_giver.master.netId, _target.master.netId, _selected)
                .Send(NetworkDestination.Server);

            _toast?.Invoke($"<style=cIsUtility>Requested: {QuirkInventory.QuirkPickupUI.MakeNiceName(_selected)}</style>", 1.2f);
            Close();
        }

        private void Close()
        {
            UICursorUtil.CloseGameCursor();
            if (Current == this) Current = null;
            Destroy(gameObject);
        }

        private void Update()
        {
            if (!_giver || !_target) { Close(); return; }
            if (Input.GetKey(KeyCode.Escape) || Input.GetKey(Config.CloseQuirkMenuHotkey.Value.MainKey)) 
            {
                Close();
            } 
        }


        private void OnDisable()
        {
            if (_subscribed && _giverInv != null)
            {
                _giverInv.OnOwnedChanged -= HandleOwnedChanged;
                _subscribed = false;
            }
        }

        private void OnDestroy()
        {
            // Ensure unsubscribe even if Unity skips OnDisable in some destroy paths
            if (_subscribed && _giverInv != null)
            {
                _giverInv.OnOwnedChanged -= HandleOwnedChanged;
                _subscribed = false;
            }

            if (Current == this) Current = null;
        }

        private void HandleOwnedChanged()
        {
            RebuildOwnedPassiveList();
            BuildRows();
            if (!_ownedPassives.Contains(_selected)) SelectFirstIfAny();
        }

        // ---------- Small UI helpers ----------
        private static T NewUI<T>(string name, Transform parent) where T : Component
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);
            return go.AddComponent<T>();
        }

        private static void StretchFull(RectTransform rt)
        {
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
        }

        private static Text NewText(string name, Transform parent, string text)
        {
            var t = NewUI<Text>(name, parent);
            t.font = ShiggyMod.Modules.ShiggyAsset.ror2Font;
            t.text = text;
            t.color = Color.white;
            t.alignment = TextAnchor.MiddleLeft;
            t.resizeTextForBestFit = true;
            return t;
        }

        private static Button BuildButton(RectTransform parent, string text, Action onClick)
        {
            var go = new GameObject(text);
            go.transform.SetParent(parent, false);

            var img = go.AddComponent<Image>();
            img.color = new Color(0.2f, 0.2f, 0.2f, 1f);

            var btn = go.AddComponent<Button>();
            btn.targetGraphic = img;

            var t = NewText("Label", go.transform, text);
            t.alignment = TextAnchor.MiddleCenter;
            StretchFull(t.rectTransform);

            btn.onClick.AddListener(() => onClick?.Invoke());

            var rt = go.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(140f, 32f);
            return btn;
        }

        public static class UICursorUtil
        {
            private static MPEventSystem GetLocalMPES()
            {
                var list = MPEventSystem.readOnlyInstancesList;
                if (list != null && list.Count > 0) return list[0];
                return EventSystem.current as MPEventSystem;
            }

            public static void OpenGameCursor()
            {
                var mpes = GetLocalMPES();
                if (!mpes) return;

                mpes.cursorOpenerCount = mpes.cursorOpenerCount + 1;
                mpes.SetCursorIndicatorEnabled(true);
                mpes.sendNavigationEvents = false;
            }

            public static void CloseGameCursor()
            {
                var mpes = GetLocalMPES();
                if (!mpes) return;

                var next = mpes.cursorOpenerCount - 1;
                mpes.cursorOpenerCount = next < 0 ? 0 : next;

                if (mpes.cursorOpenerCount == 0)
                    mpes.SetCursorIndicatorEnabled(false);

                mpes.sendNavigationEvents = true;
            }
        }
    }
}
