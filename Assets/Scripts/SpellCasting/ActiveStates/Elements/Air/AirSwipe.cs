using SpellCasting;
using UnityEngine;

namespace ActiveStates.Elements.Air
{
    public class AirSwipe : ElementThrowProjectile
    {
        public override void OnEnter()
        {
            base.OnEnter();
            elementMass.SubMasses[0].transform.position = characterBody.transform.position;

            Object.Instantiate(((ElementTypeAir)elementType).GustBurstPrefab, 
                characterBody.transform.position, 
                Quaternion.LookRotation(elementMass.CenterPositionRaw - elementMass.CenterPosition));
        }
    }
}