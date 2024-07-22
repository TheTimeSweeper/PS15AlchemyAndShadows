using SpellCasting;
using SpellCasting.Projectiles;

namespace ActiveStates.Elements.Fire
{
    public class FireSwirl : ElementSpawnProjectile
    {
        protected override ProjectileController prefab => ((ElementTypeFire)elementType).PoolPrefab;
    }
}