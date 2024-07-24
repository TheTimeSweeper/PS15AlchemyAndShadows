using UnityEngine;
using UnityEngine.Events;

namespace SpellCasting.Projectiles
{
    public abstract class ProjectileOverlap : MonoBehaviour, IProjectileSubComponent
    {
        [SerializeField]
        protected Hitbox hitbox;
        protected OverlapAttack overlapAttack;

        public ProjectileController Controller { get; set; }
    }

    public class ProjectileOverlapAttack : ProjectileOverlap, IProjectileDormant
    {
        [SerializeField]
        private float damageCoefficient = 1;

        [SerializeField]
        private float resetInterval = -1;

        [SerializeField]
        private UnityEvent impactEvent;

        [SerializeField]
        private bool destroyOnImpact;
        [SerializeField]
        private float destroyOnImpactGraceTime = 0.2f;

        private float _destoryOnImpactTim;
        private float _repeatTim;

        private bool _impacted;

        public void Init()
        {
            overlapAttack = new OverlapAttack
            {
                Hitbox = hitbox,
                Damage = Controller.BaseDamage * damageCoefficient,
                Owner = Controller.Owner.gameObject,
                Team = Controller.TeamIndex
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