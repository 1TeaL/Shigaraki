// QuirkUI.cs — runtime-built, singleton picker UI for Shiggy
// Uses server-authoritative equip via QuirkEquip.RequestApplyFromClient -> EquipLoadoutRequest

using ExtraSkillSlots;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Skills;
using RoR2.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ShiggyMod.Modules.Quirks.QuirkRegistry;

namespace ShiggyMod.Modules.Quirks
{
    public class QuirkUI : MonoBehaviour
    {
        // ---------- Singleton ----------
        public static QuirkUI Current { get; private set; }
        public static bool IsOpen => Current != null;
        private bool _subscribed;

        public static QuirkUI Show(CharacterBody body, ExtraSkillLocator extras, Action<string, float> toast = null)
        {
            if (!body) return null;
            if (Current) return Current; // already open

            var root = new GameObject("Shiggy_QuirkUI_Root");
            UnityEngine.Object.DontDestroyOnLoad(root);

            Current = root.AddComponent<QuirkUI>();
            Current._body = body;
            Current._extras = extras;
            Current._toast = toast;
            Current.BuildUI();
            return Current;
        }

        // ---------- Internals ----------
        private CharacterBody _body;
        private ExtraSkillLocator _extras;
        private Action<string, float> _toast;

        private Canvas _canvas;

        // slot selection (QuirkId.None means empty)
        private QuirkId _selPrimary = QuirkId.None;
        private QuirkId _selSecondary = QuirkId.None;
        private QuirkId _selUtility = QuirkId.None;
        private QuirkId _selSpecial = QuirkId.None;
        private QuirkId _selE1 = QuirkId.None;
        private QuirkId _selE2 = QuirkId.None;
        private QuirkId _selE3 = QuirkId.None;
        private QuirkId _selE4 = QuirkId.None;

        // slot buttons we update with chosen names
        private Button _btnPrimary, _btnSecondary, _btnUtility, _btnSpecial, _btnE1, _btnE2, _btnE3, _btnE4;

        // canonical pool for the picker
        private List<QuirkId> _activePool;

        // Always include Shiggy's base actives in the picker
        private static readonly QuirkId[] BaseShiggyActives =
        {
            QuirkId.Shiggy_DecayActive,
            QuirkId.Shiggy_AirCannonActive,
            QuirkId.Shiggy_BulletLaserActive,
            QuirkId.Shiggy_MultiplierActive,
        };

        // ---------- UI ----------
        private void BuildUI()
        {
            // Canvas
            var canvasGO = new GameObject("Canvas");
            canvasGO.transform.SetParent(this.transform, false);
            _canvas = canvasGO.AddComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var scaler = canvasGO.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);

            canvasGO.AddComponent<GraphicRaycaster>();

            // Dimmer (also closes on click)
            var dim = NewUI<Image>("Dim", canvasGO.transform);
            dim.color = new Color(0f, 0f, 0f, 0.65f);
            StretchFull(dim.rectTransform);
            var dimBtn = dim.gameObject.AddComponent<Button>();
            dimBtn.onClick.AddListener(Close);

            // Panel
            var panel = NewUI<Image>("Panel", canvasGO.transform);
            panel.color = new Color(0.08f, 0.08f, 0.08f, 0.95f);
            var prt = panel.rectTransform;
            prt.sizeDelta = new Vector2(900f, 420f);
            prt.anchorMin = prt.anchorMax = new Vector2(0.5f, 0.5f);
            prt.anchoredPosition = Vector2.zero;

            // Title
            var title = NewText("Title", panel.transform, "Quirk Loadout");
            title.alignment = TextAnchor.MiddleCenter;
            title.fontSize = 28;
            var trt = title.rectTransform;
            trt.anchorMin = new Vector2(0f, 1f);
            trt.anchorMax = new Vector2(1f, 1f);
            trt.pivot = new Vector2(0.5f, 1f);
            trt.sizeDelta = new Vector2(0f, 46f);
            trt.anchoredPosition = new Vector2(0f, -10f);

            // Two columns
            var left = NewUI<Image>("Left", panel.transform);
            left.color = new Color(1f, 1f, 1f, 0.05f);
            var lrt = left.rectTransform;
            lrt.anchorMin = new Vector2(0f, 0f);
            lrt.anchorMax = new Vector2(0.5f, 1f);
            lrt.offsetMin = new Vector2(14f, 60f);
            lrt.offsetMax = new Vector2(-7f, -60f);

            var right = NewUI<Image>("Right", panel.transform);
            right.color = new Color(1f, 1f, 1f, 0.05f);
            var rrt = right.rectTransform;
            rrt.anchorMin = new Vector2(0.5f, 0f);
            rrt.anchorMax = new Vector2(1f, 1f);
            rrt.offsetMin = new Vector2(7f, 60f);
            rrt.offsetMax = new Vector2(-14f, -60f);

            float ly = -10f;
            _btnPrimary = AddSlotRow(left.transform, ref ly, "Primary", () => OpenPicker("Primary", q => { _selPrimary = q; UpdateSlotButton(_btnPrimary, q); }));
            _btnSecondary = AddSlotRow(left.transform, ref ly, "Secondary", () => OpenPicker("Secondary", q => { _selSecondary = q; UpdateSlotButton(_btnSecondary, q); }));
            _btnUtility = AddSlotRow(left.transform, ref ly, "Utility", () => OpenPicker("Utility", q => { _selUtility = q; UpdateSlotButton(_btnUtility, q); }));
            _btnSpecial = AddSlotRow(left.transform, ref ly, "Special", () => OpenPicker("Special", q => { _selSpecial = q; UpdateSlotButton(_btnSpecial, q); }));

            float ry = -10f;
            _btnE1 = AddSlotRow(right.transform, ref ry, "Extra 1", () => OpenPicker("Extra 1", q => { _selE1 = q; UpdateSlotButton(_btnE1, q); }));
            _btnE2 = AddSlotRow(right.transform, ref ry, "Extra 2", () => OpenPicker("Extra 2", q => { _selE2 = q; UpdateSlotButton(_btnE2, q); }));
            _btnE3 = AddSlotRow(right.transform, ref ry, "Extra 3", () => OpenPicker("Extra 3", q => { _selE3 = q; UpdateSlotButton(_btnE3, q); }));
            _btnE4 = AddSlotRow(right.transform, ref ry, "Extra 4", () => OpenPicker("Extra 4", q => { _selE4 = q; UpdateSlotButton(_btnE4, q); }));

            // Bottom buttons
            var btnRow = NewUI<HorizontalLayoutGroup>("Buttons", panel.transform);
            btnRow.childAlignment = TextAnchor.MiddleRight;
            btnRow.spacing = 8f;
            var btnRowRT = (RectTransform)btnRow.transform;
            btnRowRT.anchorMin = new Vector2(0f, 0f);
            btnRowRT.anchorMax = new Vector2(1f, 0f);
            btnRowRT.pivot = new Vector2(1f, 0f);
            btnRowRT.sizeDelta = new Vector2(-22f, 40f);
            btnRowRT.anchoredPosition = new Vector2(-11f, 12f);

            BuildButton(btnRowRT, "Confirm", OnConfirm);
            BuildButton(btnRowRT, "Cancel", Close);

            // Build the selectable pool
            RebuildActivePool();

            // Pre-select from current equipped
            InitializeSelectionsFromCurrentLoadout();
            CaptureCurrentLoadout();

            // Initial labels
            UpdateAllSlotButtons();

            // Show cursor
            UICursorUtil.OpenGameCursor();

            OnEnable();
        }

        // === Pool builders ===

        private void RebuildActivePool()
        {
            var pool = new HashSet<QuirkId>();

            foreach (var q in BaseShiggyActives)
                pool.Add(q);

            foreach (var q in QuirkInventory.Owned)
                if (TryGet(q, out var rec) && rec.Skill)
                    pool.Add(q);

            if (Modules.Config.StartWithAllQuirks != null && Modules.Config.StartWithAllQuirks.Value)
                foreach (var kv in QuirkRegistry.All)
                    if (kv.Value.Skill)
                        pool.Add(kv.Key);

            _activePool = new List<QuirkId> { QuirkId.None };
            if (pool.Count == 0)
                _activePool.AddRange(BaseShiggyActives);
            else
                _activePool.AddRange(pool.OrderBy(id => QuirkInventory.QuirkPickupUI.MakeNiceName(id)));

            Debug.Log("[QuirkUI] pool=" + string.Join(", ", _activePool.Select(QuirkInventory.QuirkPickupUI.MakeNiceName)));
        }

        private void RebuildPickablePool() => RebuildActivePool();

        private void UpdateAllSlotButtons()
        {
            UpdateSlotButton(_btnPrimary, _selPrimary);
            UpdateSlotButton(_btnSecondary, _selSecondary);
            UpdateSlotButton(_btnUtility, _selUtility);
            UpdateSlotButton(_btnSpecial, _selSpecial);
            UpdateSlotButton(_btnE1, _selE1);
            UpdateSlotButton(_btnE2, _selE2);
            UpdateSlotButton(_btnE3, _selE3);
            UpdateSlotButton(_btnE4, _selE4);
        }

        // ---------- Slot rows & picker ----------
        private Button AddSlotRow(Transform parent, ref float y, string label, Action onClick)
        {
            var lab = NewText($"{label}_Label", parent, label);
            var lrt = lab.rectTransform;
            lrt.anchorMin = new Vector2(0f, 1f);
            lrt.anchorMax = new Vector2(0f, 1f);
            lrt.pivot = new Vector2(0f, 1f);
            lrt.sizeDelta = new Vector2(140f, 28f);
            lrt.anchoredPosition = new Vector2(8f, y);

            var bg = NewUI<Image>($"{label}_BG", parent);
            bg.color = new Color(1f, 1f, 1f, 0.08f);
            var brt = bg.rectTransform;
            brt.anchorMin = new Vector2(0f, 1f);
            brt.anchorMax = new Vector2(1f, 1f);
            brt.pivot = new Vector2(0f, 1f);
            brt.sizeDelta = new Vector2(-160f, 28f);
            brt.anchoredPosition = new Vector2(150f, y);

            var btn = bg.gameObject.AddComponent<Button>();
            btn.targetGraphic = bg;

            var labelText = NewText("Text", bg.transform, "(None)");
            labelText.alignment = TextAnchor.MiddleLeft;
            var tr = labelText.rectTransform;
            StretchFull(tr);
            tr.offsetMin = new Vector2(8f, 0f);

            btn.onClick.AddListener(() => onClick?.Invoke());

            y -= 34f;
            return btn;
        }

        private void UpdateSlotButton(Button btn, QuirkId q)
        {
            var t = btn.GetComponentInChildren<Text>();
            t.text = q == QuirkId.None ? "(None)" : QuirkInventory.QuirkPickupUI.MakeNiceName(q);
        }

        private void OpenPicker(string title, Action<QuirkId> onPicked)
        {
            QuirkRegistry.LateRebindIfMissing();
            RebuildActivePool();

            var modal = new GameObject("Picker").AddComponent<Image>();
            modal.transform.SetParent(_canvas.transform, false);
            modal.color = new Color(0f, 0f, 0f, 0.6f);
            var mrt = modal.rectTransform; StretchFull(mrt);

            var panel = NewUI<Image>("Panel", modal.transform);
            panel.color = new Color(0.12f, 0.12f, 0.12f, 0.98f);
            var prt = panel.rectTransform;
            prt.sizeDelta = new Vector2(520f, 560f);
            prt.anchorMin = prt.anchorMax = new Vector2(0.5f, 0.5f);
            prt.anchoredPosition = Vector2.zero;

            var ttl = NewText("Title", panel.transform, $"Pick {title}");
            ttl.alignment = TextAnchor.MiddleCenter;
            ttl.fontSize = 24;
            var trt = ttl.rectTransform;
            trt.anchorMin = new Vector2(0f, 1f);
            trt.anchorMax = new Vector2(1f, 1f);
            trt.pivot = new Vector2(0.5f, 1f);
            trt.sizeDelta = new Vector2(0f, 44f);
            trt.anchoredPosition = new Vector2(0f, -8f);


            // ScrollRect root
            var scrollGO = new GameObject("ScrollRect");
            scrollGO.transform.SetParent(panel.transform, false);

            var scrollRT = scrollGO.AddComponent<RectTransform>();
            scrollRT.anchorMin = new Vector2(0f, 0f);
            scrollRT.anchorMax = new Vector2(1f, 1f);
            scrollRT.offsetMin = new Vector2(10f, 60f);
            scrollRT.offsetMax = new Vector2(-10f, -60f);

            // IMPORTANT: a Graphic so it can receive raycasts
            var scrollImg = scrollGO.AddComponent<Image>();
            scrollImg.color = new Color(0f, 0f, 0f, 0f); // fully transparent is fine

            var scroll = scrollGO.AddComponent<ScrollRect>();
            scroll.horizontal = false;
            scroll.vertical = true;

            // Viewport as *child* of ScrollRect
            var viewportGO = new GameObject("Viewport");
            viewportGO.transform.SetParent(scrollGO.transform, false);
            var viewportRT = viewportGO.AddComponent<RectTransform>();
            viewportGO.AddComponent<RectMask2D>();
            var vimg = viewportGO.AddComponent<Image>();
            vimg.color = new Color(0f, 0f, 0f, 0.2f);
            viewportRT.anchorMin = new Vector2(0f, 0f);
            viewportRT.anchorMax = new Vector2(1f, 1f);
            viewportRT.offsetMin = Vector2.zero;
            viewportRT.offsetMax = Vector2.zero;

            scroll.viewport = viewportRT;

            // Content as child of Viewport
            var contentGO = new GameObject("Content");
            contentGO.transform.SetParent(viewportGO.transform, false);
            var crt = contentGO.AddComponent<RectTransform>();
            crt.anchorMin = new Vector2(0f, 1f);
            crt.anchorMax = new Vector2(1f, 1f);
            crt.pivot = new Vector2(0.5f, 1f);

            scroll.content = crt;

            float y = -6f;
            foreach (var q in _activePool)
            {
                var row = NewUI<Image>($"Row_{q}", contentGO.transform);
                row.color = new Color(1f, 1f, 1f, 0.06f);
                var rrt = row.rectTransform;
                rrt.anchorMin = new Vector2(0f, 1f);
                rrt.anchorMax = new Vector2(1f, 1f);
                rrt.pivot = new Vector2(0.5f, 1f);
                rrt.sizeDelta = new Vector2(0f, 30f);
                rrt.anchoredPosition = new Vector2(0f, y);
                y -= 32f;

                var btn = row.gameObject.AddComponent<Button>();
                btn.targetGraphic = row;

                var lab = NewText("Label", row.transform, q == QuirkId.None ? "(None)" : QuirkInventory.QuirkPickupUI.MakeNiceName(q));
                lab.alignment = TextAnchor.MiddleLeft;
                var lrt = lab.rectTransform; StretchFull(lrt); lrt.offsetMin = new Vector2(8f, 0f);

                btn.onClick.AddListener(() =>
                {
                    onPicked?.Invoke(q);
                    Destroy(modal.gameObject);
                });
            }
            crt.sizeDelta = new Vector2(0f, Mathf.Max(0f, -y + 6f));

            var closeBtn = BuildButton((RectTransform)panel.transform, "Close", () => Destroy(modal.gameObject));
            var cbrt = closeBtn.GetComponent<RectTransform>();
            cbrt.anchorMin = new Vector2(1f, 0f);
            cbrt.anchorMax = new Vector2(1f, 0f);
            cbrt.pivot = new Vector2(1f, 0f);
            cbrt.anchoredPosition = new Vector2(-12f, 12f);
        }

        // ---------- Confirm / Close ----------
        private void OnConfirm()
        {
            // If player left anything blank, fill with base defaults
            EnsureDefaultSelections();

            if (_body == null || _body.master == null)
            {
                _toast?.Invoke("<style=cDeath>No body/master to equip.</style>", 2f);
                Close();
                return;
            }

            var loadout = new SelectedQuirkLoadout
            {
                Primary = _selPrimary,
                Secondary = _selSecondary,
                Utility = _selUtility,
                Special = _selSpecial,
                Extra1 = _selE1,
                Extra2 = _selE2,
                Extra3 = _selE3,
                Extra4 = _selE4,
                PassiveToggles = null
            };

            // Send to server (host client included). No authority gating here.
            QuirkEquip.RequestApplyFromClient(_body, loadout);

            _toast?.Invoke("<style=cIsUtility>Quirks equipped!</style>", 1.8f);
            Close();
        }

        // Fill None with your base kit so QuirkRegistry.Get(None) never happens downstream
        private void EnsureDefaultSelections()
        {
            if (_selPrimary == QuirkId.None) _selPrimary = QuirkId.Shiggy_DecayActive;
            if (_selSecondary == QuirkId.None) _selSecondary = QuirkId.Shiggy_BulletLaserActive;
            if (_selUtility == QuirkId.None) _selUtility = QuirkId.Shiggy_AirCannonActive;
            if (_selSpecial == QuirkId.None) _selSpecial = QuirkId.Shiggy_MultiplierActive;
        }

        private void Close()
        {
            UICursorUtil.CloseGameCursor();
            if (Current == this) Current = null;
            Destroy(gameObject);
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(Config.CloseQuirkMenuHotkey.Value.MainKey))
                Close();
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
            t.font = ShiggyAsset.ror2Font;
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

        private void OnEnable()
        {
            if (!_subscribed)
            {
                QuirkInventory.OnOwnedChanged += HandleOwnedChanged;
                _subscribed = true;
            }
        }

        private void OnDisable()
        {
            if (_subscribed)
            {
                QuirkInventory.OnOwnedChanged -= HandleOwnedChanged;
                _subscribed = false;
            }
        }

        private void HandleOwnedChanged()
        {
            QuirkRegistry.LateRebindIfMissing();
            RebuildActivePool();
        }

        // Read current equipped skills -> prefill labels in the UI
        private void CaptureCurrentLoadout()
        {
            QuirkId q;
            var sl = _body ? _body.skillLocator : null;
            var ex = _extras ? _extras : (_body ? _body.GetComponent<ExtraSkillLocator>() : null);

            _selPrimary   = (sl && sl.primary   && sl.primary.skillDef   && QuirkRegistry.TryResolve(sl.primary.skillDef, out q))   ? q : QuirkId.None;
            _selSecondary = (sl && sl.secondary && sl.secondary.skillDef && QuirkRegistry.TryResolve(sl.secondary.skillDef, out q)) ? q : QuirkId.None;
            _selUtility   = (sl && sl.utility   && sl.utility.skillDef   && QuirkRegistry.TryResolve(sl.utility.skillDef, out q))   ? q : QuirkId.None;
            _selSpecial   = (sl && sl.special   && sl.special.skillDef   && QuirkRegistry.TryResolve(sl.special.skillDef, out q))   ? q : QuirkId.None;

            if (ex)
            {
                _selE1 = (ex.extraFirst  && ex.extraFirst.skillDef  && QuirkRegistry.TryResolve(ex.extraFirst.skillDef,  out q)) ? q : QuirkId.None;
                _selE2 = (ex.extraSecond && ex.extraSecond.skillDef && QuirkRegistry.TryResolve(ex.extraSecond.skillDef, out q)) ? q : QuirkId.None;
                _selE3 = (ex.extraThird  && ex.extraThird.skillDef  && QuirkRegistry.TryResolve(ex.extraThird.skillDef,  out q)) ? q : QuirkId.None;
                _selE4 = (ex.extraFourth && ex.extraFourth.skillDef && QuirkRegistry.TryResolve(ex.extraFourth.skillDef, out q)) ? q : QuirkId.None;
            }
        }

        private void InitializeSelectionsFromCurrentLoadout()
        {
            QuirkId From(GenericSkill g)
            {
                if (g && g.skillDef && QuirkRegistry.TryResolve(g.skillDef, out var qid))
                    return qid;
                return QuirkId.None;
            }

            var sl = _body ? _body.skillLocator : null;
            if (sl)
            {
                _selPrimary = From(sl.primary);
                _selSecondary = From(sl.secondary);
                _selUtility = From(sl.utility);
                _selSpecial = From(sl.special);
            }
            if (_extras)
            {
                _selE1 = From(_extras.extraFirst);
                _selE2 = From(_extras.extraSecond);
                _selE3 = From(_extras.extraThird);
                _selE4 = From(_extras.extraFourth);
            }
        }

        // ---------- Cursor helper ----------
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
