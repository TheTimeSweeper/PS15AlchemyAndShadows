using SpellCasting;

namespace ActiveStates.Elements.Earth
{

    public class EarthSwipe : ElementThrowProjectile
    {
        protected override DamageTypeIndex GetDamageType()
        {
            return DamageTypeIndex.EARTH | DamageTypeIndex.STUN;
        }
    }
}