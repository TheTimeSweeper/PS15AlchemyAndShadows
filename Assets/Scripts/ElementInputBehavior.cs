namespace SpellCasting
{
    public abstract class ElementInputBehavior
    {
        protected ElementType elementType;
        public ElementType ElementType { get => elementType; set => elementType = value; }
        protected InputState input;
        protected CommonComponentsHolder commonComponents;
        protected InputBank inputBank;

        public ElementInputBehavior(ElementTypeIndex element_, InputState input_, CommonComponentsHolder commonComponents_)
        {
            elementType = ElementCatalog.ElementTypes[element_];
            input = input_;
            commonComponents_ = commonComponents;
            inputBank = commonComponents.InputBank;
        }

        public ElementInputBehavior(ElementType element_, InputState input_, CommonComponentsHolder commonComponents_)
        {
            elementType = element_;
            input = input_;
            commonComponents = commonComponents_;
            inputBank = commonComponents.InputBank;
        }

        public abstract void Update();
        public virtual void FixedUpdate() { }
    }
}