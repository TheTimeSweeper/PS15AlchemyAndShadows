using ActiveStates;
using ActiveStates.Elements;
using System;

namespace SpellCasting
{
    public class ElementMassManipulationBehavior : ElementInputBehavior
    {
        public ElementMassManipulationBehavior(CommonComponentsHolder commonComponents_) : base(commonComponents_) { }

        private ElementMass _currentMass;

        public override void Update()
        {
            if (currentCastingElement == null)
                return;

            if (input.JustReleased)
            {
                if (_currentMass != null)
                {
                    ElementActionState actionState = inputBank.GetFirstQualifiedElementAction(currentCastingElement.ElementActions);
                    if (actionState != null)
                    {
                        inputBank.ResetGestures();
                        BaseElementMassState newState = currentCastingElement.CreateElementMassState(actionState.GestureState, _currentMass);
                        if (newState != null)
                        {
                            _currentMass.ActiveStateMachine.setState(newState);
                        }
                    }
                    else
                    {
                        BaseElementMassState newState = currentCastingElement.CreateElementMassState(currentCastingElement.MassLetGoState, _currentMass);
                        if (newState != null)
                        {
                            _currentMass.ActiveStateMachine.setState(newState);
                        }
                    }
                }
            }

            if (input.Down)
            {
                if (_currentMass == null || _currentMass.Casted)
                {
                    _currentMass = UnityEngine.Object.Instantiate(currentCastingElement.ElementMassPrefab);
                    _currentMass.Init(currentCastingElement);
                    _currentMass.ActiveStateMachine.CommonComponents = commonComponents;
                    _currentMass.ActiveStateMachine.setState(currentCastingElement.CreateElementMassState(currentCastingElement.MassSpawnState, _currentMass));
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