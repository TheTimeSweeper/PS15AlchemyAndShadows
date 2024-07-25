namespace SpellCasting
{
    public class ElementSelectionBehaviour : ElementInputBehavior
    {
        public ElementSelectionBehaviour(CommonComponentsHolder commonComponents_) : base(commonComponents_) { }

        public override void Update()
        {
            //just pressed a button , set its element
            if (inputBank.CurrentPrimaryInput != null && inputBank.CurrentPrimaryInput.JustPressed)
            {
                caster.CurrentCastingInput = inputBank.CurrentPrimaryInput;
                caster.CurrentCastingElement = caster.InputToElement.TryGetValueDefault(inputBank.CurrentPrimaryInput);
            }

            //pressed a second button while controlling an element. set to combined element
            if (currentCastingElement != null && inputBank.OrderedHeldInputs.Count > 1)
            {
                ElementType secondElement = caster.InputToElement.TryGetValueDefault(inputBank.OrderedHeldInputs[1]);
                ElementType combinationElement = ElementCatalog.TryCombineElements(currentCastingElement, secondElement);
                if (combinationElement != null)
                {
                    caster.CurrentCastingElement = combinationElement;
                }
            }

            //released the original element input. 
            if (currentCastingElement != null && input.JustReleased)
            {
                caster.CurrentCastingInput = null;
                caster.CurrentCastingElement = null;
            }
        }
    }
}