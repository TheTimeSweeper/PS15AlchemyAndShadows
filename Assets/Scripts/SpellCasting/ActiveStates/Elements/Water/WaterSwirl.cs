using SpellCasting;
using SpellCasting.Projectiles;

namespace ActiveStates.Elements.Water
{
    public class WaterSwirl : ElementSpawnProjectile
    {
        protected override ProjectileController prefab => elementType.PoolPrefab;

        protected override DamageTypeIndex GetDamageType()
        {
            return DamageTypeIndex.WATER;
        }
    }
}