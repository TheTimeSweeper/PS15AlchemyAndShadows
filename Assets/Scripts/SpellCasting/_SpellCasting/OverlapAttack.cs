using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpellCasting
{
    public class OverlapAttack
    {
        public GameObject OwnerGameObject { get; set; }
        public CharacterBody OwnerBody { get; set; }
        public Hitbox Hitbox { get; set; }
        public float Damage { get; set; }
        public TeamIndex Team { get; set; }
        public TeamTargetType TeamTargeting { get; set; } = TeamTargetType.OTHER;
        public DamageInfo DamageInfo { get; set; }

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

                    if (_alreadyHitTargets.Contains(healthComponent))
                        continue;

                    //if team is undefined, just hit everything
                    if (Team != default)
                    {
                        bool validHit = false;
                        switch (TeamTargeting)
                        {
                            case var targeting when TeamTargeting.HasFlag(TeamTargetType.SELF):
                                if (healthComponent.gameObject == OwnerGameObject)
                                    validHit = true;
                                break;

                                //JAM friendlyfiremanager?
                            case var targeting when TeamTargeting.HasFlag(TeamTargetType.OTHER):
                                if (Util.GetTeamIndex(hurtbox.HealthComponent) != Team)
                                    validHit = true;
                                break;

                            case var targeting when TeamTargeting.HasFlag(TeamTargetType.ALLY):
                                if (Util.GetTeamIndex(hurtbox.HealthComponent) == Team)
                                    validHit = true;
                                break;
                        }
                        if (!validHit)
                        {
                            continue;
                        }
                    }

                    _alreadyHitTargets.Add(healthComponent);
                    hit = true;

                    if(DamageInfo == null)
                    {
                        DamageInfo = new DamageInfo { AttackerObject = OwnerGameObject, AttackerBody = OwnerBody, Value = Damage };
                    }

                    if (Damage > 0)
                    {
                        healthComponent.TakeDamage(DamageInfo);
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