using ActiveStates;
using System;
using System.Xml.Schema;
using UnityEngine;
namespace SpellCasting
{
    public class Wiard : Caster
    {
        [SerializeField]
        private InputBank inputBank;

        [SerializeField]
        private CommonComponentsHolder commonComponents;

        [SerializeField]
        private ElementType m1Element;
        public ElementType M1Element {
            get => m1Element;
            set
            {
                UpdateElement(m1Element, value);
                m1Element = value;
            }
        }

        [SerializeField]
        private ElementType m2Element;
        public ElementType M2Element { 
            get => m2Element;
            set
            {
                UpdateElement(m2Element, value);
                m2Element = value;
            }
        }

        [SerializeField]
        private ElementType shiftElement;
        public ElementType ShiftElement { 
            get => shiftElement;
            set
            {
                UpdateElement(shiftElement, value);
                shiftElement = value;
            }
        }

        [SerializeField]
        private ElementType spaceElement;
        public ElementType SpaceElement { 
            get => spaceElement;
            set
            {
                UpdateElement(spaceElement, value);
                spaceElement = value;
            }
        }

        private void Awake()
        {
            if (m1Element)
            {
                _elementInputBehaviors.Add(new ElementMassManipulationBehavior(m1Element, inputBank.M1, commonComponents));
                _elementInputBehaviors.Add(new ElementCursorIndicatorBehavior(m1Element, inputBank.M1, commonComponents));
                _elementInputBehaviors.Add(new ElementColorBehavior(m1Element, inputBank.M1, commonComponents));
            }
            if (m2Element)
            {
                _elementInputBehaviors.Add(new ElementMassManipulationBehavior(m2Element, inputBank.M2, commonComponents));
                _elementInputBehaviors.Add(new ElementCursorIndicatorBehavior(m2Element, inputBank.M2, commonComponents));
                _elementInputBehaviors.Add(new ElementColorBehavior(m1Element, inputBank.M1, commonComponents));
            }

            if (shiftElement)
            {
                _elementInputBehaviors.Add(new ElementMassManipulationBehavior(shiftElement, inputBank.Shift, commonComponents));
                _elementInputBehaviors.Add(new ElementCursorIndicatorBehavior(shiftElement, inputBank.Shift, commonComponents));
                _elementInputBehaviors.Add(new ElementColorBehavior(shiftElement, inputBank.Shift, commonComponents));
            }
            if (spaceElement)
            {
                _elementInputBehaviors.Add(new ElementMassManipulationBehavior(spaceElement, inputBank.Space, commonComponents));
                _elementInputBehaviors.Add(new ElementCursorIndicatorBehavior(spaceElement, inputBank.Space, commonComponents));
                _elementInputBehaviors.Add(new ElementColorBehavior(spaceElement, inputBank.Space, commonComponents));
            }
        }
    }
}