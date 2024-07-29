using SpellCasting.Projectiles;

namespace ActiveStates.Elements.Earth
{
    public class EarthSwirl : ElementSpawnProjectile
    {
        protected override ProjectileController prefab => elementType.ExplosionPrefab;
    }
}