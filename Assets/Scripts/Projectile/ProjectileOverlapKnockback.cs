using UnityEngine;

namespace SpellCasting.Projectiles
{
    public class ProjectileOverlapKnockback : ProjectileOverlap, IProjectileDormant
    {
        [SerializeField]
        private float force = 10;

        [SerializeField]
        private float succCloseLimit = 3;

        private Vector3 _center;

        public void Init(ProjectileController controller)
        {
            overlapAttack = new OverlapAttack { Hitbox = hitbox, Owner = controller.Owner.gameObject };
            _center = transform.position;
        }

        void FixedUpdate()
        {
            overlapAttack.Fire();
            for (int i = 0; i < overlapAttack.HitTargets.Count; i++)
            {
                CommonComponentsHolder commonComponents = overlapAttack.HitTargets[i].CommonComponents;
                if (commonComponents != null && commonComponents.FixedMotorDriver != null)
                {
                    Transform motorTransform = commonComponents.FixedMotorDriver.transform;
                    Vector3 pushVector = motorTransform.position - _center;
                    if (force < 0 && pushVector.magnitude < succCloseLimit)
                        continue;

                    commonComponents.FixedMotorDriver.AddedMotion = pushVector * force;
                }
            }
        }
    }
}