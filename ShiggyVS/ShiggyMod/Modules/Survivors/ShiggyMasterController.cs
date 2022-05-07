using EntityStates;
using ExtraSkillSlots;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShiggyMod.Modules.Survivors
{
    [RequireComponent(typeof(CharacterBody))]
    [RequireComponent(typeof(TeamComponent))]
    [RequireComponent(typeof(InputBankTest))]
    public class ShiggyMasterController : MonoBehaviour
    {
        public ShiggyController Shiggycon;
        public ShiggyMasterController Shiggymastercon;
        private CharacterMaster characterMaster;
        private CharacterBody body;


		private ExtraSkillLocator extraskillLocator;
		public bool alphacontructpassiveDef;
		public bool beetlepassiveDef;
		public bool pestpassiveDef;
		public bool verminpassiveDef;
		public bool guppassiveDef;
		public bool hermitcrabpassiveDef;
		public bool larvapassiveDef;
		public bool lesserwisppassiveDef;
		public bool lunarexploderpassiveDef;
		public bool minimushrumpassiveDef;
		public bool roboballminibpassiveDef;
		public bool voidbarnaclepassiveDef;
		public bool voidjailerpassiveDef;
		public bool stonetitanpassiveDef;
		public bool magmawormpassiveDef;
		public bool overloadingwormpassiveDef;

		public bool alloyvultureflyDef;
		public bool beetleguardslamDef;
		public bool bisonchargeDef;
		public bool bronzongballDef;
		public bool clayapothecarymortarDef;
		public bool claytemplarminigunDef;
		public bool greaterwispballDef;
		public bool impblinkDef;
		public bool jellyfishnovaDef;
		public bool lemurianfireballDef;
		public bool lunargolemshotsDef;
		public bool lunarwispminigunDef;
		public bool parentteleportDef;
		public bool stonegolemlaserDef;
		public bool voidreaverportalDef;
		public bool beetlequeenshotgunDef;
		public bool grovetenderhookDef;
		public bool claydunestriderballDef;
		public bool soluscontrolunityknockupDef;
		public bool xiconstructbeamDef;
		public bool voiddevastatorhomingDef;
		public bool scavengerthqwibDef;

		public SkillDef[] skillListToOverrideOnRespawn;

		private void Awake()
        {

			On.RoR2.CharacterBody.Start += CharacterBody_Start;

			skillListToOverrideOnRespawn = new SkillDef[8];
		}

		public void writeToSkillList(SkillDef skillDef, int index)
		{
			skillListToOverrideOnRespawn[index] = skillDef;
		}

		private void Start()
        {


            characterMaster = gameObject.GetComponent<CharacterMaster>();
            CharacterBody self = characterMaster.GetBody();

            Shiggymastercon = characterMaster.gameObject.GetComponent<ShiggyMasterController>();
            Shiggycon = self.gameObject.GetComponent<ShiggyController>();

        }


        private void CharacterBody_Start(On.RoR2.CharacterBody.orig_Start orig, CharacterBody self)
        {
            orig.Invoke(self);


            if (self.master.gameObject.GetComponent<ShiggyMasterController>())
            {
                if (self.master.bodyPrefab == BodyCatalog.FindBodyPrefab("ShiggyBody"))
                {

                }


            }

        }


        private void CharacterModel_Awake(On.RoR2.CharacterModel.orig_Awake orig, CharacterModel self)
        {
            orig(self);
            if (self.gameObject.name.Contains("ShiggyDisplay"))
            {

				alphacontructpassiveDef = false;
				beetlepassiveDef = false;
				pestpassiveDef = false;
				verminpassiveDef = false;
				guppassiveDef = false;
				hermitcrabpassiveDef = false;
				larvapassiveDef = false;
				lesserwisppassiveDef = false;
				lunarexploderpassiveDef = false;
				minimushrumpassiveDef = false;
				roboballminibpassiveDef = false;
				voidbarnaclepassiveDef = false;
				voidjailerpassiveDef = false;

				stonetitanpassiveDef = false;
				magmawormpassiveDef = false;
				overloadingwormpassiveDef = false;


				alloyvultureflyDef = false;
				beetleguardslamDef = false;
				bisonchargeDef = false;
				bronzongballDef = false;
				clayapothecarymortarDef = false;
				claytemplarminigunDef = false;
				greaterwispballDef = false;
				impblinkDef = false;
				jellyfishnovaDef = false;
				lemurianfireballDef = false;
				lunargolemshotsDef = false;
				lunarwispminigunDef = false;
				parentteleportDef = false;
				stonegolemlaserDef = false;
				voidreaverportalDef = false;

				beetlequeenshotgunDef = false;
				grovetenderhookDef = false;
				claydunestriderballDef = false;
				soluscontrolunityknockupDef = false;
				xiconstructbeamDef = false;
				voiddevastatorhomingDef = false;
				scavengerthqwibDef = false;

			}

        }


        private void FixedUpdate()
        {
            characterMaster = gameObject.GetComponent<CharacterMaster>();
            CharacterBody self = characterMaster.GetBody();



        }

    }
}
