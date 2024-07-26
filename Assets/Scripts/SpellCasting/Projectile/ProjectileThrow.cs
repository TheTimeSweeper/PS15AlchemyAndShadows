using UnityEngine;

namespace SpellCasting.Projectiles
{
    public class ProjectileThrow : MonoBehaviour, IProjectileSubComponent, IProjectileDormant
    {
        public FireProjectileInfo ProjectileInfo { get; set; }

        [SerializeField]
        private Rigidbody rigidBody;

        void Reset()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        public void ProjectileWake()
        {
            transform.position = ProjectileInfo.StartPosition;
            transform.rotation = Quaternion.LookRotation(ProjectileInfo.AimDirection, Vector3.up);
            rigidBody.isKinematic = false;
            rigidBody.linearVelocity = ProjectileInfo.AimDirection;
        }
    }
}