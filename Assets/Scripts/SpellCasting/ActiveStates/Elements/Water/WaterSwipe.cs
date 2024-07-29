using SpellCasting;
using SpellCasting.Projectiles;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace ActiveStates.Elements.Water
{
    public class WaterSwirl : ElementSpawnProjectile
    {
        protected override ProjectileController prefab => elementType.PoolPrefab;

        protected override DamageTypeIndex GetDamageType()
        {
            return DamageTypeIndex.WATER;
        }
    }

    public class WaterSwipe : ElementThrowProjectile
    {
        public override void OnEnter()
        {
            Vector3 sideShift = Vector3.Cross(Vector3.up, inputBank.AimDirection) * (elementType as ElementTypeWater).SwipeSpawnSideDistance;

            addMass(sideShift);
            addMass(-sideShift);

            base.OnEnter();
        }

        protected override DamageTypeIndex getDamageType()
        {
            return DamageTypeIndex.WATER;
        }

        private void addMass(Vector3 positionShift)
        {
            ElementSubMass firstMass = elementMass.SubMasses[0];
            positionShift *= firstMass.CurrentMass;
            ElementSubMass newMass;
            elementMass.SubMasses.Add(newMass = 
                UnityEngine.Object.Instantiate(
                    firstMass, 
                    firstMass.transform.position + positionShift,
                    Quaternion.LookRotation(inputBank.AimDirection, Vector3.up),
                    firstMass.transform.parent));
            
        }
    }
}