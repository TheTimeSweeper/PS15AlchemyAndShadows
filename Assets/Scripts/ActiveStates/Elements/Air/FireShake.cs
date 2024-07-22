using SpellCasting;
using SpellCasting.Projectiles;
using UnityEngine;

namespace ActiveStates.Elements.Fire
{
    public class FireShake : ElementSpawnProjectile
    {
        protected override ProjectileController prefab => ((ElementTypeFire)elementType).ExplosionPrefab;
    }
}