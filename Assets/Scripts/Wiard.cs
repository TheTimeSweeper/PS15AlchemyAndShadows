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
                _elementManipulations.Add(new ElementManipulation(m1Element, inputBank.M1));
            }
            if (m2Element)
            {
                _elementManipulations.Add(new ElementManipulation(m2Element, inputBank.M2));
            }
            if (shiftElement)
            {
                _elementManipulations.Add(new ElementManipulation(shiftElement, inputBank.Shift));
            }
            if (spaceElement)
            {
                _elementManipulations.Add(new ElementManipulation(spaceElement, inputBank.Space));
            }
        }
    }
}