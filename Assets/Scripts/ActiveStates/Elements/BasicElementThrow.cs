namespace ActiveStates.Elements
{

    public class BasicElementThrow : BaseElementMassState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            for (int i = 0; i < elementMass.SubMasses.Count; i++)
            {
                elementMass.SubMasses[i].JAMThrow(inputBank.AimDirection * elementType.MassVelocityMultiplier);
            }
            elementMass.Casted = true;
        }
    }
}
