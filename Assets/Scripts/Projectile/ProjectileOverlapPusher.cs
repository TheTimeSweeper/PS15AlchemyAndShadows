using UnityEngine;

namespace SpellCasting.Projectiles
{
    public class ProjectileOverlapPusher : ProjectileOverlap, IProjectileDormant
    {
        [SerializeField]
        private Rigidbody rigidBody;

        [SerializeField]
        private TeamTargetType teamTargeting = TeamTargetType.OTHER;

        public void Init()
        {
            overlapAttack = new OverlapAttack
            {
                Hitbox = hitbox,
                Team = Controller.TeamIndex,
                TeamTargeting = teamTargeting
            };
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
                    commonComponents.FixedMotorDriver.OverrideVelocity = Vector3.zero;
                }
            }
        }
    }
}