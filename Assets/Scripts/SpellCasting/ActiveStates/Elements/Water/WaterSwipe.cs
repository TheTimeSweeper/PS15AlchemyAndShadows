using SpellCasting;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace ActiveStates.Elements.Water
{
    public class WaterSwipe : ElementThrowProjectile
    {
        public override void OnEnter()
        {
            Vector3 sideShift = Vector3.Cross(Vector3.up, inputBank.AimDirection) * (elementType as ElementTypeWater).SwipeSpawnSideDistance;

            ElementSubMass firstMass = elementMass.SubMasses[0];

            AddMass(sideShift * firstMass.CurrentMass);
            AddMass(-sideShift * firstMass.CurrentMass);

            base.OnEnter();
        }

        protected override DamageTypeIndex GetDamageType()
        {
            return DamageTypeIndex.WATER;
        }
    }
}