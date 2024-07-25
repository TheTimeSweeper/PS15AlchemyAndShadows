using ActiveStates;
using System;
using System.Xml.Schema;
using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;
namespace SpellCasting
{
    public class Wiard : Caster, IHasCommonComponents
    {
        [SerializeField]
        private InputBank inputBank;

        [SerializeField]
        private CommonComponentsHolder commonComponents;
        public CommonComponentsHolder CommonComponents => commonComponents;

        [SerializeField]
        private ElementType m1Element;
        public ElementType M1Element {
            get => m1Element;
            set
            {
                //UpdateElement(m1Element, value);
                m1Element = value;
            }
        }

        [SerializeField]
        private ElementType m2Element;
        public ElementType M2Element { 
            get => m2Element;
            set
            {
                //UpdateElement(m2Element, value);
                m2Element = value;
            }
        }

        [SerializeField]
        private ElementType shiftElement;
        public ElementType ShiftElement { 
            get => shiftElement;
            set
            {
                //UpdateElement(shiftElement, value);
                shiftElement = value;
            }
        }

        [SerializeField]
        private ElementType spaceElement;
        public ElementType SpaceElement { 
            get => spaceElement;
            set
            {
                //UpdateElement(spaceElement, value);
                spaceElement = value;
            }
        }

        private void Awake()
        {
            _elementInputBehaviors.Add(new ElementMassManipulationBehavior(commonComponents));
            _elementInputBehaviors.Add(new ElementSelectionBehaviour(commonComponents));
            _elementInputBehaviors.Add(new ElementCursorIndicatorBehavior(commonComponents));
            _elementInputBehaviors.Add(new ElementColorBehavior(commonComponents));

            InputToElement.Add(inputBank.M1, m1Element);
            InputToElement.Add(inputBank.M2, m2Element);
            InputToElement.Add(inputBank.Shift, shiftElement);
            InputToElement.Add(inputBank.Space, spaceElement);

            //ElementToInput.Add(m1Element, inputBank.M1);
            //ElementToInput.Add(m2Element, inputBank.M2);
            //ElementToInput.Add(shiftElement, inputBank.Shift);
            //ElementToInput.Add(spaceElement, inputBank.Space);
        }
    }
}