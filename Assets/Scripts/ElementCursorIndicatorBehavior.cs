using UnityEngine;
using UnityEngine.Windows;

namespace SpellCasting
{


    public class ElementCursorIndicatorBehavior : ElementInputBehavior
    {         
        public ElementCursorIndicatorBehavior(ElementTypeIndex element_, InputState input_, CommonComponentsHolder commonComponents_) : base(element_, input_, commonComponents_) { }
        public ElementCursorIndicatorBehavior(ElementType element_, InputState input_, CommonComponentsHolder commonComponents_) : base(element_, input_, commonComponents_) { }

        private CursorIndicator _currentIndicator;
        private CursorIndicator currentIndicator
        {
            get
            {
                if (_currentIndicator == null)
                {
                    _currentIndicator = Object.Instantiate(elementType.CursorIndicatorPrefab);
                    _currentIndicator.Color = elementType.ElementColor;
                }
                return _currentIndicator;
            }
        }
        private Color _elementColor;

        public override void Update()
        {
            if (currentIndicator != null)
            {
                if (input.Down)
                {
                    _currentIndicator.transform.position = inputBank.AimPoint;
                    _currentIndicator.gameObject.SetActive(true);
                }
                else
                {
                    _currentIndicator.gameObject.SetActive(false);
                }
            }
        }
    }
}