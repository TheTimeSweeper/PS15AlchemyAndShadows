using System.Collections.Generic;
using UnityEngine;

namespace SpellCasting
{
    public class OverlapAttack
    {
        public GameObject Owner { get; set; }
        public Hitbox Hitbox { get; set; }
        public float Damage { get; set; }

        private List<HealthComponent> _alreadyHitTargets = new List<HealthComponent>();
        public List<HealthComponent> HitTargets => _alreadyHitTargets;

        public bool Fire()
        {
            bool hit = false;

            Collider[] colliders = Hitbox.DoOverlap();

            for (int i = 0; i < colliders.Length; i++)
            {
                Collider collider = colliders[i];
                HurtBox hurtbox = collider.GetComponent<HurtBox>();
                if (hurtbox != null)
                {
                    HealthComponent healthComponent = hurtbox.HealthComponent;
                    if (healthComponent.gameObject == Owner)
                        continue;
                    if (_alreadyHitTargets.Contains(healthComponent))
                        continue;

                    _alreadyHitTargets.Add(healthComponent);
                    hit = true;

                    if (Damage > 0)
                    {
                        healthComponent.TakeDamage(Damage);
                    }
                }
            }

            return hit;
        }

        public void ResetHits()
        {
            _alreadyHitTargets.Clear();
        }
    }
}