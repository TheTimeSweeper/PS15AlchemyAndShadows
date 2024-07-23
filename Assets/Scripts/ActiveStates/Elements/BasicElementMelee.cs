using SpellCasting;

namespace ActiveStates.Elements
{
    public class BasicElementMelee : BaseElementMassState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            elementMass.Fizzle();

            stateMachineLocator.MainStateMachine.setState(ActiveStateCatalog.InstantiateState(elementType.BodyMeleeState));
        }
    }
}
