using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DittoMod.Modules.Survivors
{
	public class DittoController : MonoBehaviour
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
		public DittoMasterController dittomastercon;

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
			if (!characterMaster.gameObject.GetComponent<DittoMasterController>())
			{
				dittomastercon = characterMaster.gameObject.AddComponent<DittoMasterController>();
			}


			//dittomastercon.assaultvest = false;
			//dittomastercon.choiceband = false;
			//dittomastercon.choicescarf = false;
			//dittomastercon.choicespecs = false;
			//dittomastercon.leftovers = false;
			//dittomastercon.lifeorb = false;
			//dittomastercon.luckyegg = false;
			//dittomastercon.rockyhelmet = false;
			//dittomastercon.scopelens = false;
			//dittomastercon.shellbell = false;
			//dittomastercon.assaultvest2 = false;
			//dittomastercon.choiceband2 = false;
			//dittomastercon.choicescarf2 = false;
			//dittomastercon.choicespecs2 = false;
			//dittomastercon.leftovers2 = false;
			//dittomastercon.lifeorb2 = false;
			//dittomastercon.luckyegg2 = false;
			//dittomastercon.rockyhelmet2 = false;
			//dittomastercon.scopelens2 = false;
			//dittomastercon.shellbell2 = false;
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
			if (characterBody.HasBuff(DittoMod.Modules.Assets.voidelitebuff))
			{
				characterBody.RemoveBuff(DittoMod.Modules.Assets.voidelitebuff);
			}
			if (characterBody.HasBuff(DittoMod.Modules.Assets.mendingelitebuff))
			{
				characterBody.RemoveBuff(DittoMod.Modules.Assets.mendingelitebuff);
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

			//if (characterBody.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_ASSAULTVEST_NAME" && !assaultvest)
   //         {
   //             dittomastercon.assaultvest = true;
   //             assaultvest = true;
   //             characterBody.AddBuff(Modules.Buffs.assaultvestBuff);
   //         }
   //         if (characterBody.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_CHOICEBAND_NAME" && !choiceband)
   //         {
   //             dittomastercon.choiceband = true;
   //             choiceband = true;
   //             characterBody.AddBuff(Modules.Buffs.choicebandBuff);
   //         }
   //         if (characterBody.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_CHOICESCARF_NAME" && !choicescarf)
   //         {
   //             dittomastercon.choicescarf = true;
   //             choicescarf = true;
   //             characterBody.AddBuff(Modules.Buffs.choicescarfBuff);
   //         }
   //         if (characterBody.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_CHOICESPECS_NAME" && !choicespecs)
   //         {
   //             dittomastercon.choicespecs = true;
   //             choicespecs = true;
   //             characterBody.AddBuff(Modules.Buffs.choicespecsBuff);
   //         }
   //         if (characterBody.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_LEFTOVERS_NAME" && !leftovers)
   //         {
   //             dittomastercon.leftovers = true;
   //             leftovers = true;
   //             characterBody.AddBuff(Modules.Buffs.leftoversBuff);
   //         }
   //         if (characterBody.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_LIFEORB_NAME" && !lifeorb)
   //         {
   //             dittomastercon.lifeorb = true;
   //             lifeorb = true;
   //             characterBody.AddBuff(Modules.Buffs.lifeorbBuff);
   //         }
   //         //if (characterBody.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_LUCKYEGG_NAME" && !luckyegg)
   //         //{
   //         //    dittomastercon.luckyegg = true;
   //         //    luckyegg = true;
   //         //    characterBody.AddBuff(Modules.Buffs.luckyeggBuff);
   //         //}
   //         if (characterBody.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_ROCKYHELMET_NAME" && !rockyhelmet)
   //         {
   //             dittomastercon.rockyhelmet = true;
   //             rockyhelmet = true;
   //             characterBody.AddBuff(Modules.Buffs.rockyhelmetBuff);
   //         }
   //         if (characterBody.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_SCOPELENS_NAME" && !scopelens)
   //         {
   //             dittomastercon.scopelens = true;
   //             scopelens = true;
   //             characterBody.AddBuff(Modules.Buffs.scopelensBuff);
   //         }
   //         if (characterBody.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_SHELLBELL_NAME" && !shellbell)
   //         {
   //             dittomastercon.shellbell = true;
   //             shellbell = true;
   //             characterBody.AddBuff(Modules.Buffs.shellbellBuff);
   //         }
   //         if (characterBody.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_ASSAULTVEST_NAME" && !assaultvest2)
   //         {
   //             dittomastercon.assaultvest2 = true;
   //             assaultvest2 = true;
   //             characterBody.AddBuff(Modules.Buffs.assaultvestBuff);
   //         }
   //         if (characterBody.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_CHOICEBAND_NAME" && !choiceband2)
   //         {
   //             dittomastercon.choiceband2 = true;
   //             choiceband2 = true;
   //             characterBody.AddBuff(Modules.Buffs.choicebandBuff);
   //         }
   //         if (characterBody.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_CHOICESCARF_NAME" && !choicescarf2)
   //         {
   //             dittomastercon.choicescarf2 = true;
   //             choicescarf2 = true;
   //             characterBody.AddBuff(Modules.Buffs.choicescarfBuff);
   //         }
   //         if (characterBody.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_CHOICESPECS_NAME" && !choicespecs2)
   //         {
   //             dittomastercon.choicespecs2 = true;
   //             choicespecs2 = true;
   //             characterBody.AddBuff(Modules.Buffs.choicespecsBuff);
   //         }
   //         if (characterBody.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_LEFTOVERS_NAME" && !leftovers2)
   //         {
   //             dittomastercon.leftovers2 = true;
   //             leftovers2 = true;
   //             characterBody.AddBuff(Modules.Buffs.leftoversBuff);
   //         }
   //         if (characterBody.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_LIFEORB_NAME" && !lifeorb2)
   //         {
   //             dittomastercon.lifeorb2 = true;
   //             lifeorb2 = true;
   //             characterBody.AddBuff(Modules.Buffs.lifeorbBuff);
   //         }
   //         //if (characterBody.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_LUCKYEGG_NAME" && !luckyegg2)
   //         //{
   //         //    dittomastercon.luckyegg2 = true;
   //         //    luckyegg2 = true;
   //         //    characterBody.AddBuff(Modules.Buffs.luckyeggBuff);
   //         //}
   //         if (characterBody.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_ROCKYHELMET_NAME" && !rockyhelmet2)
   //         {
   //             dittomastercon.rockyhelmet2 = true;
   //             rockyhelmet2 = true;
   //             characterBody.AddBuff(Modules.Buffs.rockyhelmetBuff);
   //         }
   //         if (characterBody.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_SCOPELENS_NAME" && !scopelens2)
   //         {
   //             dittomastercon.scopelens2 = true;
   //             scopelens2 = true;
   //             characterBody.AddBuff(Modules.Buffs.scopelensBuff);
   //         }
   //         if (characterBody.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_SHELLBELL_NAME" && !shellbell2)
   //         {
   //             dittomastercon.shellbell2 = true;
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

