namespace ActiveStates.Elements
{

    public class BaseElementMassState : ActiveState
    {
        public ElementMass elementMass;
        public ElementType elementType;
        public InputBank TEMPInputBank;

        public void Init(ElementMass elementMass, ElementType elementType, InputBank inputBank)
        {
            this.elementMass = elementMass;
            this.elementType = elementType;
            this.TEMPInputBank = inputBank;
        }
    }
}