using R2API;

namespace ShiggyMod.Modules
{
    public static class Damage
    {
        internal static DamageAPI.ModdedDamageType shiggyDecay;

        internal static void SetupModdedDamage()
        {
            shiggyDecay = DamageAPI.ReserveDamageType();
        }
    }
}
