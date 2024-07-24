using ActiveStates;
using ActiveStates.Elements;
using System;

namespace SpellCasting
{
    public class ElementMassManipulationBehavior : ElementInputBehavior
    {
        public ElementMassManipulationBehavior(ElementTypeIndex element_, InputState input_, CommonComponentsHolder commonComponents_) : base(element_, input_, commonComponents_) { }
        public ElementMassManipulationBehavior(ElementType element_, InputState input_, CommonComponentsHolder commonComponents_) : base(element_, input_, commonComponents_) { }

        private ElementMass _currentMass;

        private ElementType TEMP_secondElement = ElementCatalog.ElementTypes[ElementTypeIndex.FIRE];

        public override void Update()
        {
            if (input.JustReleased)
            {
                if (_currentMass != null)
                {
                    ElementActionState actionState = inputBank.GetFirstQualifiedElementAction(currentElementType.ElementActions);
                    if (actionState != null)
                    {
                        inputBank.ResetGestures();
                        BaseElementMassState newState = currentElementType.CreateElementMassState(actionState.GestureState, _currentMass);
                        if (newState != null)
                        {
                            _currentMass.ActiveStateMachine.setState(newState);
                        }
                    }
                    else
                    {
                        BaseElementMassState newState = currentElementType.CreateElementMassState(currentElementType.MassLetGoState, _currentMass);
                        if (newState != null)
                        {
                            _currentMass.ActiveStateMachine.setState(newState);
                        }
                    }
                    overrideElementType = null;
                    commonComponents.Caster.CurrentCastingElement = null;
                }
            }

            if (input.JustPressed && !IsOtherElementActive())
            {
                commonComponents.Caster.CurrentCastingElement = currentElementType;
            }

            if (input.Down && commonComponents.Caster.CurrentCastingElement == currentElementType)
            {
                if (input == inputBank.Space && inputBank.Shift.JustPressed)
                {
                    if (_currentMass != null)
                    {
                        _currentMass.Fizzle();
                    }
                    overrideElementType = TEMP_secondElement;
                }

                if (_currentMass == null || _currentMass.Casted)
                {
                    _currentMass = UnityEngine.Object.Instantiate(currentElementType.ElementMassPrefab);
                    _currentMass.Init(currentElementType);
                    _currentMass.ActiveStateMachine.CommonComponents = commonComponents;
                    _currentMass.ActiveStateMachine.setState(currentElementType.CreateElementMassState(currentElementType.MassSpawnState, _currentMass));
                    _currentMass.transform.position = inputBank.AimPoint;
                }

                _currentMass.SetPosition(inputBank.AimPoint);
            }
        }

        public override void FixedUpdate()
        {
            //    if (input.Down)
            //    {
            //        if (currentMass == null || currentMass.Casted)
            //        {
            //            currentMass = UnityEngine.Object.Instantiate(elementType.ElementPrefab);
            //            currentMass.Init(elementType);

            //            currentMass.ActiveStateMachine.setState(elementType.CreateElementMassState(elementType.SpawnState, currentMass, inputBank));
            //            currentMass.transform.position = inputBank.AimPoint;
            //        }

            //        currentMass.SetPosition(inputBank.AimPoint);
            //    }
        }

        public override void OnManipluatorExit()
        {
            if(_currentMass != null)
            {
                _currentMass.Fizzle();
            }
        }

    }
}