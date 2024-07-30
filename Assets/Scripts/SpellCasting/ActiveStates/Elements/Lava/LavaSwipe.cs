using ActiveStates.Elements.Water;
using SpellCasting;

namespace ActiveStates.Elements.Lava
{
    public class LavaSwipe : WaterSwipe
    {
        protected override DamageTypeIndex GetDamageType()
        {
            return DamageTypeIndex.NONE;
        }
    }
}