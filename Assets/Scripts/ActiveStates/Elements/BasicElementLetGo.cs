namespace ActiveStates.Elements
{
    public class BasicElementLetGo : BaseElementMassState
    {
        public override void OnEnter()
        {
            base.OnEnter();

            elementMass.Fizzle();

        }
    }
}
