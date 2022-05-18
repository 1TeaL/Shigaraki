using RoR2;
using System.Collections.Generic;
using R2API;
using System;

namespace ShiggyMod.Modules
{
    public static class Dots
    {
        public static DotController.DotDef decaydef;
        public static DotController.DotIndex decayDot;
        

        public static DotController.DotDef CreateDot(float interval, float damageCoefficient, DamageColorIndex damageColorIndex, BuffDef associatedBuff)
        {
            return new DotController.DotDef { interval = interval, damageCoefficient = damageCoefficient, damageColorIndex = damageColorIndex, associatedBuff = associatedBuff };
        }

        public static void RegisterDots()
        {
            decayDot = DotAPI.RegisterDotDef(CreateDot(1f, Modules.StaticValues.decayDamageCoeffecient, DamageColorIndex.DeathMark, Buffs.decayDebuff), DecayDotEffect);

        }


        public static void DecayDotEffect(DotController self, DotController.DotStack dotStack)
        {
            CharacterBody attackerBody = dotStack.attackerObject.GetComponent<CharacterBody>();
            CharacterBody victimBody = self.victimObject.GetComponent<CharacterBody>();
            if (attackerBody && victimBody)
            {
                //float damageMultiplier = Modules.StaticValues.decayDamageCoeffecient + Modules.StaticValues.decayDamageStack * victimBody.GetBuffCount(Buffs.decayDebuff);
                float damageMultiplier = Modules.StaticValues.decayDamageStack;
                float decaydamage = 0f;
                if (self.victimBody) decaydamage += attackerBody.damage;
                dotStack.damage = decaydamage * damageMultiplier;

            }
        }


    }
}
