using SpellCasting;

namespace ActiveStates.Elements
{
    public class BaseElementMassState : ActiveState
    {
        public ElementMass elementMass;
        public ElementType elementType;

        public void Init(ElementMass elementMass, ElementType elementType)
        {
            this.elementMass = elementMass;
            this.elementType = elementType;
            
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (caster == null && !machine.Destroyed)
            {
                machine.setState(ActiveStateCatalog.InstantiateState(elementType.MassLetGoState));
            }
        }
    }
}