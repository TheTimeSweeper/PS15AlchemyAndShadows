using UnityEngine;

namespace ActiveStates.Elements
{
    public class BasicElementGrow : BaseElementMassState
    {
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            elementMass.Grow(elementType.GrowthMultiplier);

            elementMass.SetPosition(inputBank.AimPoint);

            for (int i = 0; i < elementMass.SubMasses.Count; i++)
            {
                elementMass.SubMasses[i].transform.position = elementMass.CenterPosition;
            }
        }
    }
}
