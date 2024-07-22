using System.Collections.Generic;
using UnityEngine;
namespace SpellCasting
{
    public class Caster : MonoBehaviour
    {
        protected List<ElementInputBehavior> _elementInputBehaviors = new List<ElementInputBehavior>();
        public List<ElementInputBehavior> ElementInputBehaviors => _elementInputBehaviors;

        public void UpdateElement(ElementType lastElement, ElementType newElement)
        {
            for (int i = _elementInputBehaviors.Count - 1; i >= 0; i--)
            {
                if (_elementInputBehaviors[i].ElementType == lastElement)
                {
                    _elementInputBehaviors[i].ElementType = newElement;
                }
            }
        }
    }
}