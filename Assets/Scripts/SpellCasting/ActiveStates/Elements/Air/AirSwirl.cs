using SpellCasting.Projectiles;
using SpellCasting;

namespace ActiveStates.Elements.Air
{
    public class AirSwirl : ElementSpawnProjectile
    {
        protected override ProjectileController prefab => ((ElementTypeAir)elementType).SuccPrefab;
    }
}