namespace SpellCasting
{
    public class ElementColorBehavior : ElementInputBehavior
    {
        public ElementColorBehavior(ElementTypeIndex element_, InputState input_, CommonComponentsHolder commonComponents_) : base(element_, input_, commonComponents_) { }
        public ElementColorBehavior(ElementType element_, InputState input_, CommonComponentsHolder commonComponents_) : base(element_, input_, commonComponents_) { }

        public override void Update()
        {
            if (commonComponents.CharacterModel == null)
                return;

            if (input.JustPressed)
            {
                commonComponents.CharacterModel.SetElementColor(elementType.ElementColor);
            }
            if(input.JustReleased)
            {
                commonComponents.CharacterModel.RemoveElementColor(elementType.ElementColor);
            }
        }
    }
}