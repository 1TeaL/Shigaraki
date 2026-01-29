using RoR2;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ShiggyMod.Modules.Quirks
{
    /// <summary>
    /// Local-only screen-space overlay (bottom-center) that shows the aimed target's Quirk name
    /// and a ✓ checkmark if the local player already owns it.
    /// </summary>
    public class QuirkHUDOverlay : MonoBehaviour
    {
        // Attach via EnsureFor(body) from your controller.
        public CharacterBody Body { get; private set; }
        public InputBankTest InputBank { get; private set; }

        // Scan settings
        public float maxTrackingDistance = 70f;
        public float maxTrackingAngle = 20f;
        public float trackerUpdateFrequency = 10f; // Hz

        // UI
        private Canvas _canvas;
        private Text _line;
        private CanvasGroup _cg;

        // Search
        private readonly BullseyeSearch _search = new BullseyeSearch();
        private float _updateStopwatch;

        // Last shown (to avoid rebuilding string every tick)
        private HurtBox _lastHB;
        private QuirkId _lastId = QuirkId.None;
        private bool _lastOwned;

        // Inventory (master-scoped, instance)
        private CharacterMaster _master;
        private QuirkInventory _inv;
        private bool _dirty = true;

        public static QuirkHUDOverlay EnsureFor(CharacterBody body)
        {
            if (!body) return null;

            // IMPORTANT: only attach on the client that owns this body
            if (!body.hasEffectiveAuthority) return null;

            var ex = body.GetComponent<QuirkHUDOverlay>();
            if (ex) return ex;

            var comp = body.gameObject.AddComponent<QuirkHUDOverlay>();
            comp.Body = body;
            comp.InputBank = body.inputBank;
            return comp;
        }

        private void Awake()
        {
            if (Body == null) Body = GetComponent<CharacterBody>();

            // Safety: never run on non-local bodies (prevents lobby-wide overlays)
            if (Body == null || !Body.hasEffectiveAuthority)
            {
                Destroy(this);
                return;
            }

            InputBank = Body.inputBank;

            ResolveInventory();
            BuildUI();
        }

        private void OnDestroy()
        {
            UnhookInventory();

            if (_canvas)
                Destroy(_canvas.gameObject);
        }

        private void FixedUpdate()
        {
            // If body swapped / lost authority, stop.
            if (Body == null) Body = GetComponent<CharacterBody>();
            if (Body == null || !Body.hasEffectiveAuthority)
            {
                Hide();
                return;
            }

            // Config gate
            if (Modules.Config.ShowQuirkNameOverlay != null &&
                Modules.Config.ShowQuirkNameOverlay.Value == false)
            {
                Hide();
                return;
            }

            _updateStopwatch += Time.fixedDeltaTime;
            if (_updateStopwatch < 1f / Mathf.Max(1f, trackerUpdateFrequency)) return;
            _updateStopwatch = 0f;

            ResolveInventory();
            UpdateTargetAndText();
        }

        // ------------- Core -------------
        private void UpdateTargetAndText()
        {
            if (!Body || !InputBank)
            {
                Hide();
                _lastHB = null; _lastId = QuirkId.None; _lastOwned = false;
                return;
            }

            // Aim-based scan
            var aimRay = new Ray(InputBank.aimOrigin, InputBank.aimDirection);
            _search.teamMaskFilter = TeamMask.all;
            _search.filterByLoS = true;
            _search.searchOrigin = aimRay.origin;
            _search.searchDirection = aimRay.direction;
            _search.sortMode = BullseyeSearch.SortMode.Distance;
            _search.maxDistanceFilter = maxTrackingDistance;
            _search.maxAngleFilter = maxTrackingAngle;
            _search.RefreshCandidates();
            _search.FilterOutGameObject(gameObject);

            var hb = _search.GetResults().FirstOrDefault();
            if (!hb || !hb.healthComponent || !hb.healthComponent.body)
            {
                Hide();
                _lastHB = null; _lastId = QuirkId.None; _lastOwned = false;
                return;
            }

            var targetBody = hb.healthComponent.body;

            string bodyName = BodyCatalog.GetBodyName(targetBody.bodyIndex);
            if (string.IsNullOrEmpty(bodyName))
                bodyName = targetBody.baseNameToken;

            // Map body -> quirk id
            if (!QuirkTargetingMap.TryGet(bodyName, out var id) || id == QuirkId.None)
            {
                Hide();
                _line.text = "";
                _lastHB = hb; _lastId = QuirkId.None; _lastOwned = false;
                return;
            }

            // Resolve record & nice name
            QuirkRecord rec;
            bool hasRec = QuirkRegistry.TryGet(id, out rec);
            string nice = hasRec ? QuirkInventory.QuirkPickupUI.MakeNiceName(id) : id.ToString();

            bool owned = _inv != null && _inv.Has(id);

            bool changed = _dirty || (hb != _lastHB) || (id != _lastId) || (owned != _lastOwned);
            if (changed)
            {
                // Color by category
                Color c =
                    !hasRec ? new Color(1f, 1f, 1f, 0.95f) :
                    rec.Category == QuirkCategory.Passive ? new Color(0.85f, 1f, 0.85f, 1f) :
                    rec.Category == QuirkCategory.Active ? new Color(0.85f, 0.9f, 1f, 1f) :
                    new Color(1f, 1f, 1f, 0.95f);

                bool showCheck = Modules.Config.ShowOwnedCheckOverlay == null || Modules.Config.ShowOwnedCheckOverlay.Value;
                string suffix = (owned && showCheck) ? "  [\u2713]" : "";

                _line.text = nice + suffix;
                _line.color = c;

                _lastHB = hb;
                _lastId = id;
                _lastOwned = owned;
                _dirty = false; // IMPORTANT: clear dirty after rebuilding
            }

            if (string.IsNullOrEmpty(_line.text))
                Hide();
            else
                Show();
        }

        // ------------- Inventory wiring -------------
        private void ResolveInventory()
        {
            if (!Body) return;

            // Cache master
            var newMaster = Body.master;
            if (newMaster != _master)
            {
                // Body/master changed (respawn etc)
                UnhookInventory();
                _master = newMaster;
                _inv = null;
            }

            if (_master != null && _inv == null)
            {
                _inv = QuirkInventory.Ensure(_master);

                if (_inv != null)
                    _inv.OnOwnedChanged += OnInventoryChanged;

                _dirty = true;
            }
        }

        private void UnhookInventory()
        {
            if (_inv != null)
                _inv.OnOwnedChanged -= OnInventoryChanged;
        }

        private void OnInventoryChanged()
        {
            _dirty = true;
        }

        // ------------- UI build/show/hide -------------
        private void BuildUI()
        {
            var root = new GameObject("QuirkHUDOverlay_Canvas");
            // Parent doesn't really matter for ScreenSpaceOverlay; keep it on the body for cleanup grouping
            root.transform.SetParent(Body ? Body.transform : null, false);

            _canvas = root.AddComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            _canvas.sortingOrder = 5000;

            var scaler = root.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);

            root.AddComponent<GraphicRaycaster>();

            _cg = root.AddComponent<CanvasGroup>();
            _cg.interactable = false;
            _cg.blocksRaycasts = false;
            _cg.alpha = 0f;

            var panel = new GameObject("Panel").AddComponent<Image>();
            panel.transform.SetParent(_canvas.transform, false);
            panel.color = new Color(0f, 0f, 0f, 0f);
            panel.raycastTarget = false;

            var prt = panel.rectTransform;
            prt.sizeDelta = new Vector2(900f, 46f);
            prt.anchorMin = prt.anchorMax = new Vector2(0.5f, 0f);
            prt.pivot = new Vector2(0.5f, 0f);
            prt.anchoredPosition = new Vector2(0f, 42f);

            var txtGO = new GameObject("Line");
            txtGO.transform.SetParent(panel.transform, false);

            var rt = txtGO.AddComponent<RectTransform>();
            rt.anchorMin = new Vector2(0f, 0f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;

            _line = txtGO.AddComponent<Text>();
            _line.font = ShiggyAsset.ror2Font;
            _line.text = string.Empty;
            _line.alignment = TextAnchor.MiddleCenter;
            _line.color = new Color(1f, 1f, 1f, 0.95f);
            _line.resizeTextForBestFit = true;
            _line.resizeTextMinSize = 16;
            _line.resizeTextMaxSize = 30;
        }

        private void Show()
        {
            if (_cg && _cg.alpha < 1f) _cg.alpha = 1f;
        }

        private void Hide()
        {
            if (_cg && _cg.alpha > 0f) _cg.alpha = 0f;
        }
    }
}
