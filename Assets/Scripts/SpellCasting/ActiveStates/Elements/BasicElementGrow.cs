using UnityEngine;

namespace ActiveStates.Elements
{
    public class BasicElementGrow : BaseElementMassState
    {
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            elementMass.Grow(elementType.MassGrowthMultiplier);

            elementMass.SetPosition(inputBank.AimPoint, caster.transform);

            for (int i = 0; i < elementMass.SubMasses.Count; i++)
            {
                //Vector3 lastPosition = elementMass.SubMasses[i].transform.position;

                elementMass.SubMasses[i].transform.position = elementMass.CenterPosition;

                //elementMass.SubMasses[i].transform.rotation = Quaternion.LookRotation(elementMass.SubMasses[i].transform.position - lastPosition, Vector3.up);
            }
        }
    }
}
