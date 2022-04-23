using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShiggyMod.Modules.Survivors
{
	public class ShiggyController : MonoBehaviour
	{
		public float maxTrackingDistance = 40f;
		public float maxTrackingAngle = 30f;
		public float trackerUpdateFrequency = 10f;
		private HurtBox trackingTarget;
		private CharacterBody characterBody;
		private InputBankTest inputBank;
		private float trackerUpdateStopwatch;
		private Indicator indicator;
		public bool transformed;
		private readonly BullseyeSearch search = new BullseyeSearch();
		private CharacterMaster characterMaster;
		private CharacterBody origCharacterBody;
		private string origName;
		public ShiggyMasterController Shiggymastercon;

		public bool assaultvest;
		public bool choiceband;
		public bool choicescarf;
		public bool choicespecs;
		public bool leftovers;
		public bool lifeorb;
		public bool luckyegg;
		public bool rockyhelmet;
		public bool scopelens;
		public bool shellbell;
		public bool assaultvest2;
		public bool choiceband2;
		public bool choicescarf2;
		public bool choicespecs2;
		public bool leftovers2;
		public bool lifeorb2;
		public bool luckyegg2;
		public bool rockyhelmet2;
		public bool scopelens2;
		public bool shellbell2;

		private int buffCountToApply;


		private void Awake()
		{
			indicator = new Indicator(gameObject, LegacyResourcesAPI.Load<GameObject>("Prefabs/HuntressTrackingIndicator"));
			//On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;
			
			characterBody = gameObject.GetComponent<CharacterBody>();
			inputBank = gameObject.GetComponent<InputBankTest>();
			assaultvest = false;
			choiceband = false;
			choicescarf = false;
			choicespecs = false;
			leftovers = false;
			lifeorb = false;
			luckyegg = false;
			rockyhelmet = false;
			scopelens = false;
			shellbell = false;
			assaultvest2 = false;
			choiceband2 = false;
			choicescarf2 = false;
			choicespecs2 = false;
			leftovers2 = false;
			lifeorb2 = false;
			luckyegg2 = false;
			rockyhelmet2 = false;
			scopelens2 = false;
			shellbell2 = false;

			buffCountToApply = 0;

		}

		private void Start()
		{

			assaultvest = false;
			choiceband = false;
			choicescarf = false;
			choicespecs = false;
			leftovers = false;
			lifeorb = false;
			luckyegg = false;
			rockyhelmet = false;
			scopelens = false;
			shellbell = false;
			assaultvest2 = false;
			choiceband2 = false;
			choicescarf2 = false;
			choicespecs2 = false;
			leftovers2 = false;
			lifeorb2 = false;
			luckyegg2 = false;
			rockyhelmet2 = false;
			scopelens2 = false;
			shellbell2 = false;

			characterMaster = characterBody.master;
			if (!characterMaster.gameObject.GetComponent<ShiggyMasterController>())
			{
				Shiggymastercon = characterMaster.gameObject.AddComponent<ShiggyMasterController>();
			}


			//Shiggymastercon.assaultvest = false;
			//Shiggymastercon.choiceband = false;
			//Shiggymastercon.choicescarf = false;
			//Shiggymastercon.choicespecs = false;
			//Shiggymastercon.leftovers = false;
			//Shiggymastercon.lifeorb = false;
			//Shiggymastercon.luckyegg = false;
			//Shiggymastercon.rockyhelmet = false;
			//Shiggymastercon.scopelens = false;
			//Shiggymastercon.shellbell = false;
			//Shiggymastercon.assaultvest2 = false;
			//Shiggymastercon.choiceband2 = false;
			//Shiggymastercon.choicescarf2 = false;
			//Shiggymastercon.choicespecs2 = false;
			//Shiggymastercon.leftovers2 = false;
			//Shiggymastercon.lifeorb2 = false;
			//Shiggymastercon.luckyegg2 = false;
			//Shiggymastercon.rockyhelmet2 = false;
			//Shiggymastercon.scopelens2 = false;
			//Shiggymastercon.shellbell2 = false;
			if (characterBody.HasBuff(RoR2Content.Buffs.AffixBlue))
			{
				characterBody.RemoveBuff(RoR2Content.Buffs.AffixBlue);
			}
			if (characterBody.HasBuff(RoR2Content.Buffs.AffixEcho))
			{
				characterBody.RemoveBuff(RoR2Content.Buffs.AffixEcho);
			}
			if (characterBody.HasBuff(RoR2Content.Buffs.AffixHaunted))
			{
				characterBody.RemoveBuff(RoR2Content.Buffs.AffixHaunted);
			}
			if (characterBody.HasBuff(RoR2Content.Buffs.AffixLunar))
			{
				characterBody.RemoveBuff(RoR2Content.Buffs.AffixLunar);
			}
			if (characterBody.HasBuff(RoR2Content.Buffs.AffixPoison))
			{
				characterBody.RemoveBuff(RoR2Content.Buffs.AffixPoison);
			}
			if (characterBody.HasBuff(RoR2Content.Buffs.AffixRed))
			{
				characterBody.RemoveBuff(RoR2Content.Buffs.AffixRed);
			}
			if (characterBody.HasBuff(RoR2Content.Buffs.AffixWhite))
			{
				characterBody.RemoveBuff(RoR2Content.Buffs.AffixWhite);
			}
			if (characterBody.HasBuff(ShiggyMod.Modules.Assets.voidelitebuff))
			{
				characterBody.RemoveBuff(ShiggyMod.Modules.Assets.voidelitebuff);
			}
			if (characterBody.HasBuff(ShiggyMod.Modules.Assets.mendingelitebuff))
			{
				characterBody.RemoveBuff(ShiggyMod.Modules.Assets.mendingelitebuff);
			}

		}

		public HurtBox GetTrackingTarget()
		{
			return this.trackingTarget;
		}

		private void OnEnable()
		{
			this.indicator.active = true;
		}

		private void OnDisable()
		{
			this.indicator.active = false;
		}
		//public void AddToBuffCount(int numbertoadd)
		//{
		//	buffCountToApply += numbertoadd;
		//}
		//public int GetBuffCount()
		//      {
		//          return buffCountToApply;
		//      }

		private void FixedUpdate()
		{
			this.trackerUpdateStopwatch += Time.fixedDeltaTime;
			if (this.trackerUpdateStopwatch >= 1f / this.trackerUpdateFrequency)
			{
				this.trackerUpdateStopwatch -= 1f / this.trackerUpdateFrequency;
				HurtBox hurtBox = this.trackingTarget;
				Ray aimRay = new Ray(this.inputBank.aimOrigin, this.inputBank.aimDirection);
				this.SearchForTarget(aimRay);
				this.indicator.targetTransform = (this.trackingTarget ? this.trackingTarget.transform : null);
			}

			//if (characterBody.skillLocator.secondary.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_ASSAULTVEST_NAME" && !assaultvest)
   //         {
   //             Shiggymastercon.assaultvest = true;
   //             assaultvest = true;
   //             characterBody.AddBuff(Modules.Buffs.assaultvestBuff);
   //         }
   //         if (characterBody.skillLocator.secondary.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_CHOICEBAND_NAME" && !choiceband)
   //         {
   //             Shiggymastercon.choiceband = true;
   //             choiceband = true;
   //             characterBody.AddBuff(Modules.Buffs.choicebandBuff);
   //         }
   //         if (characterBody.skillLocator.secondary.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_CHOICESCARF_NAME" && !choicescarf)
   //         {
   //             Shiggymastercon.choicescarf = true;
   //             choicescarf = true;
   //             characterBody.AddBuff(Modules.Buffs.choicescarfBuff);
   //         }
   //         if (characterBody.skillLocator.secondary.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_CHOICESPECS_NAME" && !choicespecs)
   //         {
   //             Shiggymastercon.choicespecs = true;
   //             choicespecs = true;
   //             characterBody.AddBuff(Modules.Buffs.choicespecsBuff);
   //         }
   //         if (characterBody.skillLocator.secondary.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_LEFTOVERS_NAME" && !leftovers)
   //         {
   //             Shiggymastercon.leftovers = true;
   //             leftovers = true;
   //             characterBody.AddBuff(Modules.Buffs.leftoversBuff);
   //         }
   //         if (characterBody.skillLocator.secondary.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_LIFEORB_NAME" && !lifeorb)
   //         {
   //             Shiggymastercon.lifeorb = true;
   //             lifeorb = true;
   //             characterBody.AddBuff(Modules.Buffs.lifeorbBuff);
   //         }
   //         //if (characterBody.skillLocator.secondary.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_LUCKYEGG_NAME" && !luckyegg)
   //         //{
   //         //    Shiggymastercon.luckyegg = true;
   //         //    luckyegg = true;
   //         //    characterBody.AddBuff(Modules.Buffs.luckyeggBuff);
   //         //}
   //         if (characterBody.skillLocator.secondary.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_ROCKYHELMET_NAME" && !rockyhelmet)
   //         {
   //             Shiggymastercon.rockyhelmet = true;
   //             rockyhelmet = true;
   //             characterBody.AddBuff(Modules.Buffs.rockyhelmetBuff);
   //         }
   //         if (characterBody.skillLocator.secondary.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_SCOPELENS_NAME" && !scopelens)
   //         {
   //             Shiggymastercon.scopelens = true;
   //             scopelens = true;
   //             characterBody.AddBuff(Modules.Buffs.scopelensBuff);
   //         }
   //         if (characterBody.skillLocator.secondary.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_SHELLBELL_NAME" && !shellbell)
   //         {
   //             Shiggymastercon.shellbell = true;
   //             shellbell = true;
   //             characterBody.AddBuff(Modules.Buffs.shellbellBuff);
   //         }
   //         if (characterBody.skillLocator.utility.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_ASSAULTVEST_NAME" && !assaultvest2)
   //         {
   //             Shiggymastercon.assaultvest2 = true;
   //             assaultvest2 = true;
   //             characterBody.AddBuff(Modules.Buffs.assaultvestBuff);
   //         }
   //         if (characterBody.skillLocator.utility.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_CHOICEBAND_NAME" && !choiceband2)
   //         {
   //             Shiggymastercon.choiceband2 = true;
   //             choiceband2 = true;
   //             characterBody.AddBuff(Modules.Buffs.choicebandBuff);
   //         }
   //         if (characterBody.skillLocator.utility.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_CHOICESCARF_NAME" && !choicescarf2)
   //         {
   //             Shiggymastercon.choicescarf2 = true;
   //             choicescarf2 = true;
   //             characterBody.AddBuff(Modules.Buffs.choicescarfBuff);
   //         }
   //         if (characterBody.skillLocator.utility.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_CHOICESPECS_NAME" && !choicespecs2)
   //         {
   //             Shiggymastercon.choicespecs2 = true;
   //             choicespecs2 = true;
   //             characterBody.AddBuff(Modules.Buffs.choicespecsBuff);
   //         }
   //         if (characterBody.skillLocator.utility.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_LEFTOVERS_NAME" && !leftovers2)
   //         {
   //             Shiggymastercon.leftovers2 = true;
   //             leftovers2 = true;
   //             characterBody.AddBuff(Modules.Buffs.leftoversBuff);
   //         }
   //         if (characterBody.skillLocator.utility.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_LIFEORB_NAME" && !lifeorb2)
   //         {
   //             Shiggymastercon.lifeorb2 = true;
   //             lifeorb2 = true;
   //             characterBody.AddBuff(Modules.Buffs.lifeorbBuff);
   //         }
   //         //if (characterBody.skillLocator.utility.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_LUCKYEGG_NAME" && !luckyegg2)
   //         //{
   //         //    Shiggymastercon.luckyegg2 = true;
   //         //    luckyegg2 = true;
   //         //    characterBody.AddBuff(Modules.Buffs.luckyeggBuff);
   //         //}
   //         if (characterBody.skillLocator.utility.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_ROCKYHELMET_NAME" && !rockyhelmet2)
   //         {
   //             Shiggymastercon.rockyhelmet2 = true;
   //             rockyhelmet2 = true;
   //             characterBody.AddBuff(Modules.Buffs.rockyhelmetBuff);
   //         }
   //         if (characterBody.skillLocator.utility.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_SCOPELENS_NAME" && !scopelens2)
   //         {
   //             Shiggymastercon.scopelens2 = true;
   //             scopelens2 = true;
   //             characterBody.AddBuff(Modules.Buffs.scopelensBuff);
   //         }
   //         if (characterBody.skillLocator.utility.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_SHELLBELL_NAME" && !shellbell2)
   //         {
   //             Shiggymastercon.shellbell2 = true;
   //             shellbell2 = true;
   //             characterBody.AddBuff(Modules.Buffs.shellbellBuff);
   //         }

        }

		private void SearchForTarget(Ray aimRay)
		{
			this.search.teamMaskFilter = TeamMask.all;
			this.search.filterByLoS = true;
			this.search.searchOrigin = aimRay.origin;
			this.search.searchDirection = aimRay.direction;
			this.search.sortMode = BullseyeSearch.SortMode.Distance;
			this.search.maxDistanceFilter = this.maxTrackingDistance;
			this.search.maxAngleFilter = this.maxTrackingAngle;
			this.search.RefreshCandidates();
			this.search.FilterOutGameObject(base.gameObject);
			this.trackingTarget = this.search.GetResults().FirstOrDefault<HurtBox>();
		}




	}
}

