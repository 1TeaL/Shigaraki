using System;
using RoR2;
using RoR2.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShiggyMod.Modules.Survivors
{

    public class EnergySystem : MonoBehaviour
    {
        public CharacterBody characterBody;

        //UI Energymeter
        public GameObject CustomUIObject;
        public RectTransform plusChaosMeter;
        public RectTransform plusChaosMeterGlowRect;
        public Image plusChaosMeterGlowBackground;
        public HGTextMeshProUGUI plusChaosNumber;
        public HGTextMeshProUGUI quirkGetUI;
        private bool informAFOToPlayers;
        public string quirkGetString;
        public float quirkGetStopwatch;


        //Energy system
        public float maxPlusChaos;
        public float currentplusChaos;
        public float regenPlusChaos;
        public float costmultiplierplusChaos;
        public float costflatplusChaos;
        public float plusChaosDecayTimer;
        public bool SetActiveTrue;
        //bools to stop energy regen after skill used
        private bool ifEnergyUsed;
        private float energyDecayTimer;
        private bool ifEnergyRegenAllowed;

        //Energy bar glow
        private enum GlowState
        {
            STOP,
            FLASH,
            DECAY
        }
        private float decayConst;
        private float flashConst;
        private float glowStopwatch;
        private Color targetColor;
        private Color originalColor;
        private Color currentColor;
        private GlowState state;

        public void Awake()
        {
            characterBody = gameObject.GetComponent<CharacterBody>();
        }

        public void Start()
        {
            //Energy
            maxPlusChaos = StaticValues.basePlusChaos + ((characterBody.level - 1) * StaticValues.levelPlusChaos);
            currentplusChaos = maxPlusChaos;
            regenPlusChaos = maxPlusChaos * StaticValues.regenPlusChaosFraction;
            costmultiplierplusChaos = 1f;
            costflatplusChaos = 0f;
            ifEnergyRegenAllowed = true;
            ifEnergyUsed = false;

            //UI objects 
            CustomUIObject = UnityEngine.Object.Instantiate(Modules.Assets.mainAssetBundle.LoadAsset<GameObject>("shiggyCustomUI"));
            CustomUIObject.SetActive(false);
            SetActiveTrue = false;

            plusChaosMeter = CustomUIObject.transform.GetChild(0).GetComponent<RectTransform>();
            plusChaosMeterGlowBackground = CustomUIObject.transform.GetChild(1).GetComponent<Image>();
            plusChaosMeterGlowRect = CustomUIObject.transform.GetChild(1).GetComponent<RectTransform>();

            //setup the UI element for the min/max
            plusChaosNumber = this.CreateLabel(CustomUIObject.transform, "plusChaosNumber", $"{(int)currentplusChaos} / {maxPlusChaos}", new Vector2(0, -110), 24f, new Color(0.92f, 0.12f, 0.8f));

            //ui element for information below the energy
            quirkGetUI = this.CreateLabel(CustomUIObject.transform, "quirkGetString", quirkGetString, new Vector2(0, -220), 24f, Color.white);
            quirkGetUI.SetText(quirkGetString);
            quirkGetUI.enabled = true;


            // Start timer on 1f to turn off the timer.
            state = GlowState.STOP;
            decayConst = 1f;
            flashConst = 1f;
            glowStopwatch = 1f;
            originalColor = new Color(1f, 1f, 1f, 0f);
            targetColor = new Color(1f, 1f, 1f, 1f);
            currentColor = originalColor;

        }

        public void quirkGetInformation(string stringToPass, float duration)
        {
            quirkGetStopwatch = duration;
            quirkGetString = stringToPass;
            quirkGetUI.enabled = true;
            
        }

        //Creates the label.
        private HGTextMeshProUGUI CreateLabel(Transform parent, string name, string text, Vector2 position, float textScale, Color color)
        {
            GameObject gameObject = new GameObject(name);
            gameObject.transform.parent = parent;
            gameObject.AddComponent<CanvasRenderer>();
            RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
            HGTextMeshProUGUI hgtextMeshProUGUI = gameObject.AddComponent<HGTextMeshProUGUI>();
            hgtextMeshProUGUI.enabled = true;
            hgtextMeshProUGUI.text = text;
            hgtextMeshProUGUI.fontSize = textScale;
            hgtextMeshProUGUI.color = color;
            hgtextMeshProUGUI.alignment = TextAlignmentOptions.Center;
            hgtextMeshProUGUI.enableWordWrapping = false;
            rectTransform.localPosition = Vector2.zero;
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.localScale = Vector3.one;
            rectTransform.sizeDelta = Vector2.zero;
            rectTransform.anchoredPosition = position;
            return hgtextMeshProUGUI;

            //GameObject textObj;
            //if (!parent)
            //{
            //    CustomUIObject = new GameObject(name);
            //    textObj = CustomUIObject;
            //    Canvas canvas = textObj.AddComponent<Canvas>();
            //    canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            //}
            //else
            //{
            //    textObj = new GameObject(name);
            //    textObj.transform.parent = parent;

            //}

            //textObj.AddComponent<CanvasRenderer>();
            //RectTransform rectTransform = textObj.AddComponent<RectTransform>();
            //HGTextMeshProUGUI hgtextMeshProUGUI = textObj.AddComponent<HGTextMeshProUGUI>();
            //hgtextMeshProUGUI.enabled = true;
            //hgtextMeshProUGUI.text = text;
            //hgtextMeshProUGUI.fontSize = textScale;
            //hgtextMeshProUGUI.color = color;
            //hgtextMeshProUGUI.alignment = TextAlignmentOptions.Center;
            //hgtextMeshProUGUI.enableWordWrapping = false;
            //rectTransform.localPosition = Vector2.zero;
            //rectTransform.anchorMin = Vector2.zero;
            //rectTransform.anchorMax = Vector2.one;
            //rectTransform.localScale = Vector3.one;
            //rectTransform.sizeDelta = Vector2.zero;
            //rectTransform.anchoredPosition = position;
            //return hgtextMeshProUGUI;
        }

        private void CalculateEnergyStats()
        {
            //Energy updates
            if (characterBody)
            {
                maxPlusChaos = StaticValues.basePlusChaos + ((characterBody.level - 1) * StaticValues.levelPlusChaos)
                    + (StaticValues.backupGain * characterBody.master.inventory.GetItemCount(RoR2Content.Items.SecondarySkillMagazine))
                    + (StaticValues.afterburnerGain * characterBody.master.inventory.GetItemCount(RoR2Content.Items.UtilitySkillMagazine))
                    + (StaticValues.lysateGain * characterBody.master.inventory.GetItemCount(DLC1Content.Items.EquipmentMagazineVoid));

                regenPlusChaos = maxPlusChaos * StaticValues.regenPlusChaosFraction;

                costmultiplierplusChaos = (float)Math.Pow(0.75f, characterBody.master.inventory.GetItemCount(RoR2Content.Items.AlienHead));
                costflatplusChaos = (StaticValues.costFlatPlusChaosSpend * characterBody.master.inventory.GetItemCount(RoR2Content.Items.LunarBadLuck));

                if (costmultiplierplusChaos > 1f)
                {
                    costmultiplierplusChaos = 1f;
                }
            }


            //plusChaos Currently have

            //allow regen
            if (ifEnergyUsed)
            {
                if (energyDecayTimer > 1f)
                {
                    energyDecayTimer = 0f;
                    ifEnergyRegenAllowed = true;
                    ifEnergyUsed = false;
                }
                else
                {
                    ifEnergyRegenAllowed = false;
                    energyDecayTimer += Time.fixedDeltaTime;
                }
            }
            if(ifEnergyRegenAllowed)
            {
                currentplusChaos += regenPlusChaos * Time.fixedDeltaTime;
            }


            if (currentplusChaos > maxPlusChaos)
            {
                currentplusChaos = maxPlusChaos;
            }
            if (currentplusChaos < 0f)
            {
                currentplusChaos = 0f;
            }

            if (plusChaosNumber)
            {
                plusChaosNumber.SetText($"{(int)currentplusChaos} / {maxPlusChaos}");
            }

            if (plusChaosMeter)
            {
                // 2f because meter is too small probably.
                // Logarithmically scale.
                float logVal = Mathf.Log10(((maxPlusChaos / StaticValues.basePlusChaos) * 10f) + 1) * (currentplusChaos / maxPlusChaos);
                plusChaosMeter.localScale = new Vector3(2.0f * logVal, 0.05f, 1f);
                plusChaosMeterGlowRect.localScale = new Vector3(2.3f * logVal, 0.1f, 1f);
            }

            //Chat.AddMessage($"{currentplusChaos}/{maxPlusChaos}");
        }

        public void FixedUpdate()
        {
            if (characterBody.hasEffectiveAuthority)
            {
                CalculateEnergyStats();
            }

            if (characterBody.hasEffectiveAuthority && !SetActiveTrue)
            {
                CustomUIObject.SetActive(true);
                SetActiveTrue = true;
            }
        }

        public void Update()
        {
            //Debug.Log(quirkGetString+ "quirkgetstring");

            if (quirkGetUI.isActiveAndEnabled)
            {
                quirkGetUI.SetText(quirkGetString);
                if (!informAFOToPlayers)
                {
                    informAFOToPlayers = true;
                    Chat.AddMessage($"Press the [{Config.AFOHotkey.Value}] key to use <style=cIsUtility>All For One and steal quirks</style>."
                    + $" Press the [{Config.RemoveHotkey.Value}] key to <style=cIsUtility>remove quirks</style>." +
                    $" Press the [{Config.AFOGiveHotkey.Value}] key to <style=cIsUtility>give passive quirks</style>.");
                    quirkGetInformation($"Press the [{Config.AFOHotkey.Value}] key to use <style=cIsUtility>All For One and steal quirks</style>."
                    + $" Press the [{Config.RemoveHotkey.Value}] key to <style=cIsUtility>remove quirks</style>." +
                    $" Press the [{Config.AFOGiveHotkey.Value}] key to <style=cIsUtility>give passive quirks</style>.", 5f);
                }

                if (quirkGetStopwatch > 0f)
                {
                    quirkGetStopwatch -= Time.deltaTime;
                }
                else if (quirkGetStopwatch <= 0f)
                {
                    quirkGetString = "";
                }
            }

            if (state != GlowState.STOP)
            {
                glowStopwatch += Time.deltaTime;
                float lerpFraction;
                switch (state)
                {
                    // Lerp to target color
                    case GlowState.FLASH:

                        lerpFraction = glowStopwatch / flashConst;
                        currentColor = Color.Lerp(originalColor, targetColor, lerpFraction);

                        if (glowStopwatch > flashConst)
                        {
                            state = GlowState.DECAY;
                            glowStopwatch = 0f;
                        }
                        break;

                    //Lerp back to original color;
                    case GlowState.DECAY:
                        //Linearlly lerp.
                        lerpFraction = glowStopwatch / decayConst;
                        currentColor = Color.Lerp(targetColor, originalColor, lerpFraction);

                        if (glowStopwatch > decayConst)
                        {
                            state = GlowState.STOP;
                            glowStopwatch = 0f;
                        }
                        break;
                    case GlowState.STOP:
                        //State does nothing.
                        break;
                }
            }

            plusChaosMeterGlowBackground.color = currentColor;
        }


        public void SpendplusChaos(float plusChaos)
        {
            //float plusChaosflatCost = plusChaos - costflatplusChaos;
            //if (plusChaosflatCost < 0f) plusChaosflatCost = 0f;

            //float plusChaosCost = rageplusChaosCost * costmultiplierplusChaos * plusChaosflatCost;
            //if (plusChaosCost < 0f) plusChaosCost = 0f;

            currentplusChaos -= plusChaos;
            TriggerGlow(0.3f, 0.3f, Color.magenta);
            ifEnergyUsed = true;

        }
        public void GainplusChaos(float plusChaos)
        {
            //float plusChaosflatCost = plusChaos - costflatplusChaos;
            //if (plusChaosflatCost < 0f) plusChaosflatCost = 0f;

            //float plusChaosCost = rageplusChaosCost * costmultiplierplusChaos * plusChaosflatCost;
            //if (plusChaosCost < 0f) plusChaosCost = 0f;

            currentplusChaos += plusChaos;
            TriggerGlow(0.3f, 0.3f, Color.cyan);

        }

        public void TriggerGlow(float newDecayTimer, float newFlashTimer, Color newStartingColor)
        {
            decayConst = newDecayTimer;
            flashConst = newFlashTimer;
            originalColor = new Color(newStartingColor.r, newStartingColor.g, newStartingColor.b, 0f);
            targetColor = newStartingColor;
            glowStopwatch = 0f;
            state = GlowState.FLASH;
        }


        public void OnDestroy()
        {
            Destroy(CustomUIObject);
        }
    }
}

