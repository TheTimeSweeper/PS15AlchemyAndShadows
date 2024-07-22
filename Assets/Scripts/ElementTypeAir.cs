using SpellCasting.Projectiles;
using UnityEngine;

namespace SpellCasting
{
    [CreateAssetMenu(menuName = "ElementType/Air", fileName = "ElementAir")]
    public class ElementTypeAir : ElementType
    {
        [SerializeField]
        private ProjectileController succPrefab;
        public ProjectileController SuccPrefab => succPrefab;

        [SerializeField]
        private ProjectileController blowPrefab;
        public ProjectileController BlowPrefab => blowPrefab;
    }
}