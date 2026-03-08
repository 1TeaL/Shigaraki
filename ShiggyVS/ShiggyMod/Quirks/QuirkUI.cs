// QuirkUI.cs — runtime-built, singleton picker UI for Shiggy
// Uses server-authoritative equip via QuirkEquip.RequestApplyFromClient -> EquipLoadoutRequest

using ExtraSkillSlots;
using RoR2;
using RoR2.Skills;
using RoR2.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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

        // --- smooth pause on open (singleplayer only)
        private Coroutine _pauseEaseCoroutine;
        private float _preMenuTimeScale = 1f;
        private bool _didPauseForMenu;

        //apex surgery info
        private ApexSurgeryController _apexController;
        private Image _apexInfoRoot;
        private HGTextMeshProUGUI _apexInfoText;
        private float _apexInfoRefreshTimer;

        public static QuirkUI Show(CharacterBody body, ExtraSkillLocator extras, Action<string, float> toast = null)
        {
            if (!body) return null;

            // Only local player can open their UI
            if (!body.hasEffectiveAuthority) return null;

            if (Current) return Current;

            var root = new GameObject("Shiggy_QuirkUI_Root");
            UnityEngine.Object.DontDestroyOnLoad(root);

            Current = root.AddComponent<QuirkUI>();
            Current._body = body;
            Current._extras = extras;
            Current._inv = body.master ? QuirkInventory.Ensure(body.master) : null;
            Current._apexController = body.master ? body.master.GetComponent<ApexSurgeryController>() : null; //apex surgery info
            Current._toast = toast;
            Current.BuildUI();
            return Current;
        }

        // ---------- Internals ----------
        private CharacterBody _body;
        private ExtraSkillLocator _extras;
        private Action<string, float> _toast;
        private QuirkInventory _inv;
        private Canvas _canvas; 
        private Image _tooltipRoot;
        private RectTransform _tooltipRT; 
        private HGTextMeshProUGUI _tooltipText;

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

        // Local preview override key + last applied skilldefs so we can Unset cleanly
        private static readonly object s_localPreviewOverrideKey = new object();

        private SkillDef _prevPrimaryDef, _prevSecondaryDef, _prevUtilityDef, _prevSpecialDef;
        private SkillDef _prevE1Def, _prevE2Def, _prevE3Def, _prevE4Def;

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

            // tooltip top layer
            BuildTooltip();

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
            prt.sizeDelta = new Vector2(1220f, 500f);
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

            // ===== Apex info panel (far left) =====
            var apexPanel = NewUI<Image>("ApexInfoPanel", panel.transform);
            apexPanel.color = new Color(1f, 1f, 1f, 0.05f);
            var art = apexPanel.rectTransform;
            art.anchorMin = new Vector2(0f, 0f);
            art.anchorMax = new Vector2(0f, 1f);
            art.pivot = new Vector2(0f, 0.5f);
            art.offsetMin = new Vector2(14f, 72f);
            art.offsetMax = new Vector2(249f, -70f);

            var apexTitle = NewText("ApexTitle", apexPanel.transform, "Apex Adaptation");
            apexTitle.alignment = TextAnchor.MiddleCenter;
            apexTitle.fontSize = 20;
            var atrt = apexTitle.rectTransform;
            atrt.anchorMin = new Vector2(0f, 1f);
            atrt.anchorMax = new Vector2(1f, 1f);
            atrt.pivot = new Vector2(0.5f, 1f);
            atrt.sizeDelta = new Vector2(0f, 34f);
            atrt.anchoredPosition = new Vector2(0f, -8f);

            _apexInfoRoot = apexPanel;

            _apexInfoText = NewHGText("ApexInfoText", apexPanel.transform, "");
            _apexInfoText.alignment = TextAlignmentOptions.TopLeft;
            _apexInfoText.fontSize = 15f;
            _apexInfoText.enableAutoSizing = false;
            _apexInfoText.enableWordWrapping = true;

            var airt = _apexInfoText.rectTransform;
            airt.anchorMin = new Vector2(0f, 0f);
            airt.anchorMax = new Vector2(1f, 1f);
            airt.offsetMin = new Vector2(10f, 10f);
            airt.offsetMax = new Vector2(-10f, -40f);

            // ===== Content area to the right of the Apex panel =====
            var contentRoot = new GameObject("ContentRoot").AddComponent<RectTransform>();
            contentRoot.SetParent(panel.transform, false);
            contentRoot.anchorMin = new Vector2(0f, 0f);
            contentRoot.anchorMax = new Vector2(1f, 1f);
            contentRoot.offsetMin = new Vector2(263f, 72f);
            contentRoot.offsetMax = new Vector2(-14f, -70f);

            // ===== Two equal columns inside contentRoot =====
            var left = NewUI<Image>("Left", contentRoot);
            left.color = new Color(1f, 1f, 1f, 0.05f);
            var lrt = left.rectTransform;
            lrt.anchorMin = new Vector2(0f, 0f);
            lrt.anchorMax = new Vector2(0.5f, 1f);
            lrt.offsetMin = new Vector2(0f, 0f);
            lrt.offsetMax = new Vector2(-6f, 0f);

            var right = NewUI<Image>("Right", contentRoot);
            right.color = new Color(1f, 1f, 1f, 0.05f);
            var rrt = right.rectTransform;
            rrt.anchorMin = new Vector2(0.5f, 0f);
            rrt.anchorMax = new Vector2(1f, 1f);
            rrt.offsetMin = new Vector2(6f, 0f);
            rrt.offsetMax = new Vector2(0f, 0f);

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

            // ===== Bottom buttons =====
            var btnRow = NewUI<HorizontalLayoutGroup>("Buttons", panel.transform);
            btnRow.childAlignment = TextAnchor.MiddleCenter;
            btnRow.spacing = 12f;
            btnRow.childForceExpandWidth = false;
            btnRow.childForceExpandHeight = false;
            btnRow.childControlWidth = true;
            btnRow.childControlHeight = true;

            var btnRowRT = (RectTransform)btnRow.transform;
            btnRowRT.anchorMin = new Vector2(0.5f, 0f);
            btnRowRT.anchorMax = new Vector2(0.5f, 0f);
            btnRowRT.pivot = new Vector2(0.5f, 0f);
            btnRowRT.sizeDelta = new Vector2(340f, 40f);
            btnRowRT.anchoredPosition = new Vector2(0f, 18f);

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

            Subscribe();

            // Apex info
            RefreshApexInfo();

            // pause
            BeginSingleplayerSlowPause();
        }
        private void BuildTooltip()
        {
            _tooltipRoot = NewUI<Image>("Tooltip", _canvas.transform);
            _tooltipRoot.color = new Color(0f, 0f, 0f, 0.9f);
            _tooltipRT = _tooltipRoot.rectTransform;
            _tooltipRT.pivot = new Vector2(0f, 1f);
            _tooltipRT.sizeDelta = new Vector2(520f, 120f);
            _tooltipRoot.gameObject.SetActive(false);

            _tooltipText = NewHGText("TooltipText", _tooltipRoot.transform, "");
            _tooltipText.alignment = TextAlignmentOptions.TopLeft;
            _tooltipText.enableAutoSizing = false;
            _tooltipText.fontSize = 16f;
            _tooltipText.richText = true;
            _tooltipText.enableWordWrapping = true;

            // keep tooltip object above everything
            _tooltipRoot.transform.SetAsLastSibling();

            var ttr = _tooltipText.rectTransform;
            StretchFull(ttr);
            ttr.offsetMin = new Vector2(10f, 10f);
            ttr.offsetMax = new Vector2(-10f, -10f);
        }

        private void BeginSingleplayerSlowPause()
        {
            // only in real singleplayer (no MP, no host-mp)
            if (!RoR2Application.isInSinglePlayer) return;

            // If you're already paused by something else, don't touch it.
            if (Time.timeScale <= 0f) return;

            _preMenuTimeScale = Time.timeScale;
            _didPauseForMenu = true;

            if (_pauseEaseCoroutine != null) StopCoroutine(_pauseEaseCoroutine);
            _pauseEaseCoroutine = StartCoroutine(CoEaseTimeScaleToZero(0.12f)); // 120ms feels snappy
        }

        private void EndSingleplayerSlowPause()
        {
            if (!_didPauseForMenu) return;

            if (_pauseEaseCoroutine != null)
            {
                StopCoroutine(_pauseEaseCoroutine);
                _pauseEaseCoroutine = null;
            }

            // restore to what it was (normally 1)
            Time.timeScale = (_preMenuTimeScale > 0f) ? _preMenuTimeScale : 1f;
            _didPauseForMenu = false;
        }

        private IEnumerator CoEaseTimeScaleToZero(float durationSeconds)
        {
            // Unscaled time so it still runs while we slow down.
            float start = Time.timeScale;
            float t = 0f;

            while (t < durationSeconds)
            {
                t += Time.unscaledDeltaTime;
                float a = Mathf.Clamp01(t / durationSeconds);

                // Ease-out curve (fast drop, smooth finish)
                float eased = 1f - (1f - a) * (1f - a);

                // Interpolate to a tiny epsilon, then hard 0 at end.
                Time.timeScale = Mathf.Lerp(start, 0.0001f, eased);
                yield return null;
            }

            Time.timeScale = 0f; // hard pause
            _pauseEaseCoroutine = null;
        }


        private void Subscribe()
        {
            if (_subscribed) return;

            if (_inv != null)
                _inv.OnOwnedChanged += HandleOwnedChanged;

            _subscribed = true;
        }
        // === Pool builders ===

        private void RebuildActivePool()
        {
            var pool = new HashSet<QuirkId>();

            foreach (var q in BaseShiggyActives)
                pool.Add(q);

            if (_inv != null)
            {
                foreach (var q in _inv.Owned)
                    if (TryGet(q, out var rec) && rec.SkillDef != null)
                        pool.Add(q);
            }

            if (Modules.Config.StartWithAllQuirks != null && Modules.Config.StartWithAllQuirks.Value)
                foreach (var kv in QuirkRegistry.All)
                    if (kv.Value.SkillDef != null)
                        pool.Add(kv.Key);

            _activePool = new List<QuirkId> { QuirkId.None };
            if (pool.Count == 0)
                _activePool.AddRange(BaseShiggyActives);
            else
                _activePool.AddRange(pool.OrderBy(id => GetLocalizedName(id)));
            Debug.Log("[QuirkUI] pool=" + string.Join(", ", _activePool.Select(GetLocalizedName)));

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
            t.text = GetLocalizedName(q);
        }

        private void OpenPicker(string title, Action<QuirkId> onPicked)
        {
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
            scroll.scrollSensitivity = 50f; //scroll speed

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

                var forward = row.gameObject.AddComponent<ScrollDragForwarder>();
                forward.scroll = scroll;
                
                var btn = row.gameObject.AddComponent<Button>();
                btn.targetGraphic = row;

                var lab = NewText("Label", row.transform, GetLocalizedName(q));
                lab.alignment = TextAnchor.MiddleLeft;
                var lrt = lab.rectTransform; StretchFull(lrt); lrt.offsetMin = new Vector2(8f, 0f);

                btn.onClick.AddListener(() =>
                {
                    HideTooltip();
                    onPicked?.Invoke(q);
                    Destroy(modal.gameObject);
                });
                lab.raycastTarget = false;
                //hover tooltip
                var trig = row.gameObject.AddComponent<EventTrigger>();

                void AddTrigger(EventTriggerType type, Action action)
                {
                    var entry = new EventTrigger.Entry { eventID = type };
                    entry.callback.AddListener(_ => action());
                    trig.triggers.Add(entry);
                }

                AddTrigger(EventTriggerType.PointerEnter, () =>
                {
                    if (q == QuirkId.None) return;

                    string name = GetLocalizedName(q);
                    string desc = GetLocalizedDesc(q);

                    ShowTooltip($"{name}\n\n{desc}");
                });

                AddTrigger(EventTriggerType.PointerExit, HideTooltip);
            }
            crt.sizeDelta = new Vector2(0f, Mathf.Max(0f, -y + 6f));


            var closeBtn = BuildButton((RectTransform)panel.transform, "Close", () => Destroy(modal.gameObject));
            var cbrt = closeBtn.GetComponent<RectTransform>();
            cbrt.anchorMin = new Vector2(1f, 0f);
            cbrt.anchorMax = new Vector2(1f, 0f);
            cbrt.pivot = new Vector2(1f, 0f);
            cbrt.anchoredPosition = new Vector2(-12f, 12f);
        }
        private static HGTextMeshProUGUI NewHGText(string name, Transform parent, string text)
        {
            var t = NewUI<HGTextMeshProUGUI>(name, parent);
            t.text = text;
            t.color = Color.white;
            t.richText = true;
            t.enableWordWrapping = true;
            t.alignment = TextAlignmentOptions.TopLeft;
            return t;
        }
        private void ShowTooltip(string text)
        {
            if (!_tooltipRoot || !_tooltipText) return;

            _tooltipText.text = text;
            _tooltipRoot.gameObject.SetActive(true);

            // ensure top draw order every time (picker panels are created later)
            _tooltipRoot.transform.SetAsLastSibling();

            PositionTooltipAtMouse();
        }

        private void HideTooltip()
        {
            if (_tooltipRoot)
                _tooltipRoot.gameObject.SetActive(false);
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
            };

            // LOCAL PREVIEW (non-host needs this so the slots actually change immediately)
            // Server remains authoritative; this is only client-side presentation.
            ApplyLocalPreviewOverrides(loadout);

            // Send to server (host client included). No authority gating here.
            // NOTE: ideally this should send MASTER netId, not body, but keep your API for now.
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
            if (_subscribed)
            {
                if (_inv != null)
                    _inv.OnOwnedChanged -= HandleOwnedChanged;
                _subscribed = false;
            }

            ClearLocalPreviewOverrides(); // NEW

            UICursorUtil.CloseGameCursor();
            if (Current == this) Current = null;

            EndSingleplayerSlowPause();
            HideTooltip();
            Destroy(gameObject);
        }


        private void Update()
        {

            // confirm hotkey
            if (UnityEngine.Input.GetKey(Config.ConfirmQuirkMenuHotkey.Value.MainKey))
                OnConfirm();
            // cancel hotkey
            if (UnityEngine.Input.GetKey(Config.CloseQuirkMenuHotkey.Value.MainKey))
                Close();

            if (_tooltipRoot && _tooltipRoot.gameObject.activeSelf)
                PositionTooltipAtMouse();

            _apexInfoRefreshTimer += Time.unscaledDeltaTime;
            if (_apexInfoRefreshTimer >= 0.1f)
            {
                _apexInfoRefreshTimer = 0f;
                RefreshApexInfo();
            }
        }

        private void PositionTooltipAtMouse()
        {
            Vector2 pos = Input.mousePosition;
            pos.x += 20f;
            pos.y -= 20f;

            _tooltipRT.position = pos;

            float w = _tooltipRT.sizeDelta.x;
            float h = _tooltipRT.sizeDelta.y;

            _tooltipRT.position = new Vector3(
                Mathf.Clamp(_tooltipRT.position.x, 0f, Screen.width - w),
                Mathf.Clamp(_tooltipRT.position.y, h, Screen.height),
                0f
            );
        }

        private void OnDestroy()
        {
            EndSingleplayerSlowPause();
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

            var layout = go.AddComponent<LayoutElement>();
            layout.minWidth = 150f;
            layout.preferredWidth = 150f;
            layout.minHeight = 34f;
            layout.preferredHeight = 34f;
            layout.flexibleWidth = 0f;
            layout.flexibleHeight = 0f;

            var t = NewText("Label", go.transform, text);
            t.alignment = TextAnchor.MiddleCenter;
            StretchFull(t.rectTransform);

            btn.onClick.AddListener(() => onClick?.Invoke());

            return btn;
        }

        private static SkillDef GetSkillDefSafe(QuirkId q)
        {
            if (q == QuirkId.None) return null;

            // Registry-backed
            if (QuirkRegistry.TryGet(q, out var rec) && rec.SkillDef != null)
                return rec.SkillDef;

            // Fallbacks for base actives
            switch (q)
            {
                case QuirkId.Shiggy_DecayActive: return ShiggyMod.Modules.Survivors.Shiggy.decayDef;
                case QuirkId.Shiggy_AirCannonActive: return ShiggyMod.Modules.Survivors.Shiggy.aircannonDef;
                case QuirkId.Shiggy_BulletLaserActive: return ShiggyMod.Modules.Survivors.Shiggy.bulletlaserDef;
                case QuirkId.Shiggy_MultiplierActive: return ShiggyMod.Modules.Survivors.Shiggy.multiplierDef;
                default: return null;
            }
        }

        private static void UnsetIfSet(GenericSkill slot, object key, SkillDef def)
        {
            if (!slot || !def) return;
            slot.UnsetSkillOverride(key, def, GenericSkill.SkillOverridePriority.Contextual);
        }

        private static void SetOverride(GenericSkill slot, object key, SkillDef def)
        {
            if (!slot || !def) return;
            slot.SetSkillOverride(key, def, GenericSkill.SkillOverridePriority.Contextual);
        }

        private void ClearLocalPreviewOverrides()
        {
            if (_body == null) return;

            var sl = _body.skillLocator;
            var ex = _extras ? _extras : _body.GetComponent<ExtraSkillLocator>();

            // Unset previous preview overrides only (so we don't touch server overrides)
            if (sl)
            {
                UnsetIfSet(sl.primary, s_localPreviewOverrideKey, _prevPrimaryDef);
                UnsetIfSet(sl.secondary, s_localPreviewOverrideKey, _prevSecondaryDef);
                UnsetIfSet(sl.utility, s_localPreviewOverrideKey, _prevUtilityDef);
                UnsetIfSet(sl.special, s_localPreviewOverrideKey, _prevSpecialDef);
            }

            if (ex)
            {
                UnsetIfSet(ex.extraFirst, s_localPreviewOverrideKey, _prevE1Def);
                UnsetIfSet(ex.extraSecond, s_localPreviewOverrideKey, _prevE2Def);
                UnsetIfSet(ex.extraThird, s_localPreviewOverrideKey, _prevE3Def);
                UnsetIfSet(ex.extraFourth, s_localPreviewOverrideKey, _prevE4Def);
            }

            _prevPrimaryDef = _prevSecondaryDef = _prevUtilityDef = _prevSpecialDef = null;
            _prevE1Def = _prevE2Def = _prevE3Def = _prevE4Def = null;
        }

        private void ApplyLocalPreviewOverrides(SelectedQuirkLoadout loadout)
        {
            if (_body == null) return;

            var sl = _body.skillLocator;
            var ex = _extras ? _extras : _body.GetComponent<ExtraSkillLocator>();

            // Always clear our preview key first to avoid stacking
            ClearLocalPreviewOverrides();

            var pDef = GetSkillDefSafe(loadout.Primary);
            var sDef = GetSkillDefSafe(loadout.Secondary);
            var uDef = GetSkillDefSafe(loadout.Utility);
            var spDef = GetSkillDefSafe(loadout.Special);

            var e1Def = GetSkillDefSafe(loadout.Extra1);
            var e2Def = GetSkillDefSafe(loadout.Extra2);
            var e3Def = GetSkillDefSafe(loadout.Extra3);
            var e4Def = GetSkillDefSafe(loadout.Extra4);

            if (sl)
            {
                SetOverride(sl.primary, s_localPreviewOverrideKey, pDef);
                SetOverride(sl.secondary, s_localPreviewOverrideKey, sDef);
                SetOverride(sl.utility, s_localPreviewOverrideKey, uDef);
                SetOverride(sl.special, s_localPreviewOverrideKey, spDef);

                _prevPrimaryDef = pDef;
                _prevSecondaryDef = sDef;
                _prevUtilityDef = uDef;
                _prevSpecialDef = spDef;
            }

            if (ex)
            {
                SetOverride(ex.extraFirst, s_localPreviewOverrideKey, e1Def);
                SetOverride(ex.extraSecond, s_localPreviewOverrideKey, e2Def);
                SetOverride(ex.extraThird, s_localPreviewOverrideKey, e3Def);
                SetOverride(ex.extraFourth, s_localPreviewOverrideKey, e4Def);

                _prevE1Def = e1Def;
                _prevE2Def = e2Def;
                _prevE3Def = e3Def;
                _prevE4Def = e4Def;
            }

            // Force UI/skill refresh locally (helps some HUDs update instantly)
            if (_body) _body.MarkAllStatsDirty();
        }

        private void OnEnable()
        {
            if (!_subscribed)
            {
                if (_inv != null) _inv.OnOwnedChanged += HandleOwnedChanged;
                _subscribed = true;
            }
        }

        private void OnDisable()
        {
            if (_subscribed)
            {
                if (_inv != null) _inv.OnOwnedChanged -= HandleOwnedChanged;
                _subscribed = false;
            }
        }

        private void HandleOwnedChanged()
        {
            RebuildActivePool();
        }

        // Read current equipped skills -> prefill labels in the UI
        private void CaptureCurrentLoadout()
        {
            QuirkId q;
            var sl = _body ? _body.skillLocator : null;
            var ex = _extras ? _extras : (_body ? _body.GetComponent<ExtraSkillLocator>() : null);

            _selPrimary = (sl && sl.primary && sl.primary.skillDef && QuirkRegistry.QuirkLookup.TryFromSkill(sl.primary.skillDef, out q)) ? q : QuirkId.None;
            _selSecondary = (sl && sl.secondary && sl.secondary.skillDef && QuirkRegistry.QuirkLookup.TryFromSkill(sl.secondary.skillDef, out q)) ? q : QuirkId.None;
            _selUtility = (sl && sl.utility && sl.utility.skillDef && QuirkRegistry.QuirkLookup.TryFromSkill(sl.utility.skillDef, out q)) ? q : QuirkId.None;
            _selSpecial = (sl && sl.special && sl.special.skillDef && QuirkRegistry.QuirkLookup.TryFromSkill(sl.special.skillDef, out q)) ? q : QuirkId.None;

            if (ex)
            {
                _selE1 = (ex.extraFirst && ex.extraFirst.skillDef && QuirkRegistry.QuirkLookup.TryFromSkill(ex.extraFirst.skillDef, out q)) ? q : QuirkId.None;
                _selE2 = (ex.extraSecond && ex.extraSecond.skillDef && QuirkRegistry.QuirkLookup.TryFromSkill(ex.extraSecond.skillDef, out q)) ? q : QuirkId.None;
                _selE3 = (ex.extraThird && ex.extraThird.skillDef && QuirkRegistry.QuirkLookup.TryFromSkill(ex.extraThird.skillDef, out q)) ? q : QuirkId.None;
                _selE4 = (ex.extraFourth && ex.extraFourth.skillDef && QuirkRegistry.QuirkLookup.TryFromSkill(ex.extraFourth.skillDef, out q)) ? q : QuirkId.None;
            }
        }

        private void InitializeSelectionsFromCurrentLoadout()
        {
            QuirkId From(GenericSkill g)
            {
                if (g && g.skillDef && QuirkRegistry.QuirkLookup.TryFromSkill(g.skillDef, out var qid))
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
        private static string GetLocalizedName(QuirkId q)
        {
            if (q == QuirkId.None) return "(None)";

            var def = GetSkillDefSafe(q);
            if (def != null && !string.IsNullOrEmpty(def.skillNameToken))
                return Language.GetString(def.skillNameToken);

            // fallback to your nice name
            return QuirkInventory.QuirkPickupUI.MakeNiceName(q);
        }

        private static string GetLocalizedDesc(QuirkId q)
        {
            var def = GetSkillDefSafe(q);
            if (def != null && !string.IsNullOrEmpty(def.skillDescriptionToken))
                return Language.GetString(def.skillDescriptionToken);

            return string.Empty;
        }

        private class ScrollDragForwarder : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IScrollHandler
        {
            public ScrollRect scroll;

            public void OnBeginDrag(PointerEventData eventData) => scroll?.OnBeginDrag(eventData);
            public void OnDrag(PointerEventData eventData) => scroll?.OnDrag(eventData);
            public void OnEndDrag(PointerEventData eventData) => scroll?.OnEndDrag(eventData);
            public void OnScroll(PointerEventData eventData) => scroll?.OnScroll(eventData);
        }

        private void RefreshApexInfo()
        {
            if (_apexInfoText == null)
                return;

            if (_apexController == null)
            {
                _apexInfoText.text = "<color=#888888>Controller not found.</color>";
                return;
            }

            int adapt = _apexController.GetAdaptationStacks();
            int per = _apexController.GetAdaptationPerThreshold();
            int thresholds = _apexController.GetAdaptationThresholds();
            int within = adapt % per;

            int apex = _apexController.GetCurrentApexStacks();
            int cap = _apexController.GetCurrentApexCap();

            float reward = _apexController.GetAdaptRewardPerThreshold();
            float damageMult = _apexController.GetCurrentDamageMultiplier();
            float moveMult = _apexController.GetCurrentMoveMultiplier();
            float regenMult = _apexController.GetCurrentRegenMultiplier();
            float armorBonus = _apexController.GetCurrentArmorBonus();

            _apexInfoText.text =
                $"<b>Stacks</b>\n" +
                $"Adapt: <color=#FFFFFF>{adapt}</color>\n" +
                $"Progress: <color=#FFFFFF>{within}/{per}</color>\n" +
                $"Thresholds: <color=#FFFFFF>{thresholds}</color>\n" +
                $"Apex: <color=#FFFFFF>{apex}</color>\n" +
                $"Limit: <color=#FFFFFF>{cap}</color>\n\n" +
                $"<b>Per Threshold</b>\n" +
                $"Damage: <color=#A8FFB0>+{reward * 100f:0.#}%</color>\n" +
                $"Move: <color=#A8FFB0>+{reward * 100f:0.#}%</color>\n" +
                $"Regen: <color=#A8FFB0>+{reward * 100f:0.#}%</color>\n" +
                $"Armor: <color=#A8FFB0>+{reward * 20f:0.#}</color>\n\n" +
                $"<b>Current Total</b>\n" +
                $"Damage: <color=#A8FFB0>x{damageMult:0.##}</color>\n" +
                $"Move: <color=#A8FFB0>x{moveMult:0.##}</color>\n" +
                $"Regen: <color=#A8FFB0>x{regenMult:0.##}</color>\n" +
                $"Armor: <color=#A8FFB0>+{armorBonus:0.#}</color>";
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
