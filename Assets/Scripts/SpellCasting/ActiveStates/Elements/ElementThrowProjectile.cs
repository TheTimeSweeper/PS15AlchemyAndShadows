using UnityEngine;

namespace ActiveStates.Elements
{
    public class ElementThrowProjectile : BaseElementMassState 
    {
        public override void OnEnter()
        {
            base.OnEnter();

            for (int i = 0; i < elementMass.SubMasses.Count; i++)
            {
                Vector3 originalPosition = elementMass.SubMasses[i].transform.position;
                Vector3 relativePOsition = originalPosition - elementMass.CenterPosition;

                elementMass.SubMasses[i].JAMActivateProjectile(characterBody, 
                    inputBank.AimDirection * elementType.MassVelocityMultiplier,
                    GetPositionInlineWithDirectionPerpendicularly() + relativePOsition,
                    originalPosition);
            }

            elementMass.Casted = true;
        }

        protected virtual Vector3 GetPositionInlineWithDirectionPerpendicularly()
        {

            return elementMass.CenterPositionRaw;
            //Vector3 CMinusR = elementMass.CenterPosition - elementMass.CenterPositionRaw;

            //float theta = Vector3.Angle(CMinusR, inputBank.AimDirection);

            //float distanceToFinal = Mathf.Cos(theta) * CMinusR.magnitude;

            //return (distanceToFinal * -inputBank.AimDirection) + elementMass.CenterPositionRaw;
        }
    }
}
