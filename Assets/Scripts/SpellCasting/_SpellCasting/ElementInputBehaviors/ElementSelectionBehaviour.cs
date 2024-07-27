﻿using System;

namespace SpellCasting
{
    public class ElementSelectionBehaviour : ElementInputBehavior
    {
        public ElementSelectionBehaviour(CommonComponentsHolder commonComponents_) : base(commonComponents_) { }

        public override void FixedUpdate()
        {
            //Util.LogWarning($"inputBank.CurrentPrimaryInput: {inputBank.CurrentPrimaryInput}, inputBank.CurrentPrimaryInput.JustPressed: {inputBank.CurrentPrimaryInput != null && inputBank.CurrentPrimaryInput.JustPressed(this + "1")}", UnityEngine.KeyCode.Z);
            //just pressed a button , set its element
            if (inputBank.CurrentPrimaryInput != null && inputBank.CurrentPrimaryInput.JustPressed(this))
            {
                ElementType inputElement = caster.TryGetInputElement(inputBank.CurrentPrimaryInput);
                if (inputElement == null)
                    return;
                caster.CurrentCastingElement = inputElement;
                caster.CurrentCastingInput = inputBank.CurrentPrimaryInput;
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
            if (currentCastingElement != null && input.JustReleased(this))
            {
                caster.CurrentCastingInput = null;
                caster.CurrentCastingElement = null;
            }
        }
    }
}