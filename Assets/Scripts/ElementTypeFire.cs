using SpellCasting.Projectiles;
using UnityEngine;

namespace SpellCasting
{
    [CreateAssetMenu(menuName = "ElementType/Fire", fileName = "ElementFire")]
    public class ElementTypeFire : ElementType
    {
        [SerializeField]
        private ProjectileController explosionPrefab;
        public ProjectileController ExplosionPrefab => explosionPrefab;

        [SerializeField]
        private ProjectileController poolPrefab;
        public ProjectileController PoolPrefab => poolPrefab;
    }
}