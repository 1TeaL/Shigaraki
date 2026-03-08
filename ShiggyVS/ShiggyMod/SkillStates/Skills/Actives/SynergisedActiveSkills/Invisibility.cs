using R2API.Networking;
using RoR2;
using ShiggyMod.Modules;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class Invisibility : Skill
    {
        //Bandit + Hermit Crab
        public override void OnEnter()
        {
            base.OnEnter();
            exitOnDuration = true;

            Ray aimRay = base.GetAimRay();
            Util.PlaySound(EntityStates.Bandit2.StealthMode.enterStealthSound, base.gameObject);

            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(RoR2Content.Buffs.Cloak.buffIndex, 1, StaticValues.invisibilityDuration);
            }
        }

    }
}