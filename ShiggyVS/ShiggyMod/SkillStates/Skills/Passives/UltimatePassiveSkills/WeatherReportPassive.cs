using R2API.Networking;
using ShiggyMod.Modules;

namespace ShiggyMod.SkillStates
{
    public class WeatherReportPassive : Skill
    {
        //Gravitational downforce + elemental fusion
        public float baseDuration = 1f;
        public float duration;
        private string muzzleString = "RHand";

        public override void OnEnter()
        {
            base.OnEnter();
            exitOnDuration = true;

            if (!characterBody.HasBuff(Buffs.weatherReportBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.weatherReportBuff.buffIndex, 1);
            }


        }
        public override void OnExit()
        {
            base.OnExit();
        }

    }
}