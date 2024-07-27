using UnityEngine;
using UnityEngine.Events;

namespace SpellCasting.Projectiles
{
    public class ProjectileOverlapAttack : ProjectileOverlap, IProjectileDormant
    {

        [SerializeField]
        private Rigidbody rigidBody;

        [SerializeField]
        private float damageCoefficient = 1;

        [SerializeField]
        private float resetInterval = -1;

        [SerializeField]
        private bool destroyOnImpact;
        [SerializeField]
        private float destroyOnImpactGraceTime = 0.2f;

        [SerializeField]
        private UnityEvent impactEvent;

        private float _destoryOnImpactTim;
        private float _repeatTim;

        private bool _impacted;

        public void ProjectileWake()
        {
            overlapAttack = new OverlapAttack
            {
                Hitbox = hitbox,
                Damage = ProjectileInfo.Damage * damageCoefficient,
                DamageType = ProjectileInfo.DamageType,
                OwnerGameObject = ProjectileInfo.OwnerObject,
                Team = ProjectileInfo.TeamIndex,
                KnockbackDirection = rigidBody ? rigidBody.linearVelocity.normalized : default
            };
            _repeatTim = resetInterval;
        }

        void FixedUpdate()
        {
            if (resetInterval > 0)
            {
                _repeatTim -= Time.fixedDeltaTime;
                if (_repeatTim < 0)
                {
                    _repeatTim += resetInterval;

                    overlapAttack.ResetHits();
                }
            }

            bool hit = overlapAttack.Fire();
            if (!_impacted)
            {
                if (hit)
                {
                    _impacted = true;
                    impactEvent.Invoke();
                }
            }
            else if (destroyOnImpact)
            {
                _destoryOnImpactTim += Time.fixedDeltaTime;
                if (_destoryOnImpactTim > destroyOnImpactGraceTime)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}