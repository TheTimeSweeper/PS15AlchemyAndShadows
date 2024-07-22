using UnityEngine;
using UnityEngine.Events;

namespace SpellCasting.Projectiles
{
    public abstract class ProjectileOverlap : MonoBehaviour
    {
        [SerializeField]
        protected Hitbox hitbox;
        protected OverlapAttack overlapAttack;
    }

    public class ProjectileOverlapAttack : ProjectileOverlap, IProjectileDormant
    {
        [SerializeField]
        private float damageCoefficient;

        [SerializeField]
        private UnityEvent impactEvent;

        private bool _impacted;

        [SerializeField]
        private bool destroyOnImpact;
        [SerializeField]
        private float destroyOnImpactGraceTime = 0.2f;

        private float _destoryOnImpactTim;

        public void Init(ProjectileController controller)
        {
            overlapAttack = new OverlapAttack { Hitbox = hitbox, Damage = controller.BaseDamage * damageCoefficient, Owner = controller.Owner.gameObject };
        }

        void FixedUpdate()
        {
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