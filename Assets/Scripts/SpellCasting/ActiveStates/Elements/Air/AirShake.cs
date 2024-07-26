using SpellCasting;
using SpellCasting.Projectiles;

namespace ActiveStates.Elements.Air
{
    public class AirShake : ElementSpawnProjectile
    {
        protected override ProjectileController prefab => ((ElementTypeAir)elementType).BlowPrefab;
    }
}