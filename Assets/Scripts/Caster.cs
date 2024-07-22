using System.Collections.Generic;
using UnityEngine;
namespace SpellCasting
{
    public class Caster : MonoBehaviour
    {
        protected List<ElementManipulation> _elementManipulations = new List<ElementManipulation>();
        public List<ElementManipulation> ElementManipulations => _elementManipulations;

        public void UpdateElement(ElementType lastElement, ElementType newElement)
        {
            for (int i = _elementManipulations.Count - 1; i >= 0; i--)
            {
                if (_elementManipulations[i].CurrentElementType == lastElement)
                {
                    _elementManipulations[i].CurrentElementType = newElement;
                }
            }
        }
    }
}