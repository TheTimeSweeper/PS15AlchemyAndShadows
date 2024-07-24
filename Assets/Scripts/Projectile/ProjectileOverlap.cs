using UnityEngine;

namespace SpellCasting.Projectiles
{
    public abstract class ProjectileOverlap : MonoBehaviour, IProjectileSubComponent
    {
        [SerializeField]
        protected Hitbox hitbox;
        protected OverlapAttack overlapAttack;

        public ProjectileController Controller { get; set; }
    }
}