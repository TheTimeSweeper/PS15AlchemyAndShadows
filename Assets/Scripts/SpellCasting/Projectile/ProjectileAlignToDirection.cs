using UnityEngine;

namespace SpellCasting.Projectiles
{
    public class ProjectileAlignToDirection : MonoBehaviour, IProjectileSubComponent
    {
        public FireProjectileInfo ProjectileInfo { get; set; }

        void Start ()
        {
            transform.LookAt(transform.position + ProjectileInfo.AimDirection);
        }
    }
}