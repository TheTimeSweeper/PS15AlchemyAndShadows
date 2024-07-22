using UnityEngine;

namespace SpellCasting.Projectiles
{
    public class ProjectileOverlapPusher : ProjectileOverlap, IProjectileDormant
    {
        [SerializeField]
        private Rigidbody rigidBody;

        public void Init(ProjectileController controller)
        {
            overlapAttack = new OverlapAttack { Hitbox = hitbox };
        }

        void FixedUpdate()
        {
            overlapAttack.Fire();
            for (int i = 0; i < overlapAttack.HitTargets.Count; i++)
            {
                CommonComponentsHolder commonComponents = overlapAttack.HitTargets[i].CommonComponents;
                if (commonComponents != null && commonComponents.FixedMotorDriver != null)
                {
                    commonComponents.FixedMotorDriver.AddedMotion = rigidBody.velocity * Time.fixedDeltaTime;
                }
            }
        }
    }
}