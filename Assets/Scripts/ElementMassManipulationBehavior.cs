using ActiveStates;
using ActiveStates.Elements;

namespace SpellCasting
{
    public class ElementMassManipulationBehavior : ElementInputBehavior
    {
        public ElementMassManipulationBehavior(ElementTypeIndex element_, InputState input_, CommonComponentsHolder commonComponents_) : base(element_, input_, commonComponents_) { }
        public ElementMassManipulationBehavior(ElementType element_, InputState input_, CommonComponentsHolder commonComponents_) : base(element_, input_, commonComponents_) { }

        private ElementMass _currentMass;

        public override void Update()
        {
            if (input.JustReleased)
            {
                if (_currentMass != null)
                {
                    if (inputBank.LatestGesture != null)
                    {
                        inputBank.ResetGestures();
                        BaseElementMassState newState = elementType.CreateElementMassState(inputBank.LatestGesture, _currentMass);
                        if (newState != null)
                        {
                            _currentMass.ActiveStateMachine.setState(newState);
                        }
                    }
                    else
                    {
                        BaseElementMassState newState = elementType.CreateElementMassState(elementType.LetGoState, _currentMass);
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
                    _currentMass = UnityEngine.Object.Instantiate(elementType.ElementMassPrefab);
                    _currentMass.Init(elementType);
                    _currentMass.ActiveStateMachine.CommonComponents = commonComponents;
                    _currentMass.ActiveStateMachine.setState(elementType.CreateElementMassState(elementType.SpawnState, _currentMass));
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
    }
}