using SpellCasting;
using SpellCasting.Projectiles;
using UnityEngine;

namespace ActiveStates.Elements
{
    public abstract class ElementSpawnProjectile : BaseElementMassState
    {
        protected abstract ProjectileController prefab { get; }
        public override void OnEnter()
        {
            base.OnEnter();

            for (int i = 0; i < elementMass.SubMasses.Count; i++)
            {
                ProjectileController explosionProjectile = Object.Instantiate(prefab, elementMass.SubMasses[i].transform.position, Quaternion.identity);
                explosionProjectile.Init(new FireProjectileInfo
                {
                    OwnerObject = characterBody.gameObject,
                    OwnerBody = characterBody,
                    Damage = characterBody.stats.Damage,
                    PreviousPosition = elementMass.SubMasses[i].transform.position,
                    StartPosition = elementMass.SubMasses[i].transform.position,
                    AimDirection = inputBank.AimDirection,
                    TeamIndex = characterBody.teamIndex,
                    DamageType = GetDamageType()
                });
            }

            elementMass.Fizzle();
        }

        protected virtual DamageTypeIndex GetDamageType()
        {
            return DamageTypeIndex.NONE;
        }
    }
}
