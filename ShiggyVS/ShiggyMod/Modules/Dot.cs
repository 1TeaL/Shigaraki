using RoR2;
using System.Collections.Generic;
using R2API;
using System;
using ShiggyMod.Modules.Survivors;

namespace ShiggyMod.Modules
{
    public static class Dots
    {
        public static DotController.DotDef decaydef;
        public static DotController.DotIndex decayDot;
        public static ShiggyController Shiggycon;
        

        public static DotController.DotDef CreateDot(float interval, float damageCoefficient, DamageColorIndex damageColorIndex, BuffDef associatedBuff)
        {
            return new DotController.DotDef { interval = interval, damageCoefficient = damageCoefficient, damageColorIndex = damageColorIndex, associatedBuff = associatedBuff };
        }

        public static void RegisterDots()
        {
            decayDot = DotAPI.RegisterDotDef(CreateDot(1f, Modules.StaticValues.decayDamageCoefficient, DamageColorIndex.DeathMark, Buffs.decayDebuff), DecayDotEffect);

        }


        public static void DecayDotEffect(DotController self, DotController.DotStack dotStack)
        {

            CharacterBody attackerBody = dotStack.attackerObject.GetComponent<CharacterBody>();
            CharacterBody victimBody = self.victimObject.GetComponent<CharacterBody>();
            if (attackerBody && victimBody)
            {

                //float damageMultiplier = Modules.StaticValues.decayDamageCoefficient + Modules.StaticValues.decayDamageStack * victimBody.GetBuffCount(Buffs.decayDebuff);
                float damageMultiplier = Modules.StaticValues.decayDamageStack;
                float decaydamage = 0f;
                decaydamage += attackerBody.damage;
                dotStack.damage = GetMax(victimBody.healthComponent.fullCombinedHealth * Modules.StaticValues.decayDamagePercentage, decaydamage * damageMultiplier);
                dotStack.damageType = DamageType.DoT;                
                dotStack.attackerObject = attackerBody.gameObject;
                
            }
        }

        public static float GetMax(float first, float second)
        {
            return first > second ? first : second; 
        }

    }
}
