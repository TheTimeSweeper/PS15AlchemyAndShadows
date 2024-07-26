using SpellCasting;

namespace ActiveStates.Elements
{
    public class BasicElementMelee : BaseElementMassState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            elementMass.Fizzle();

            stateMachineLocator.MainStateMachine.tryInterruptState(ActiveStateCatalog.InstantiateState(elementType.BodyMeleeState), InterruptPriority.STATE_ANY);
        }
    }
}
