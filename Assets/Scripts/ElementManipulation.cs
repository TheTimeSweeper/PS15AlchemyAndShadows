using ActiveStates;
using ActiveStates.Elements;
using Unity.VisualScripting.Antlr3.Runtime;

namespace SpellCasting
{
    public class ElementManipulation
    {
        private ElementType elementType;
        public ElementType CurrentElementType { get => elementType; set => elementType = value; }

        private InputState input;

        private ElementMass currentMass;

        public ElementManipulation(ElementTypeIndex element_, InputState input_)
        {
            elementType = ElementCatalog.ElementTypes[element_];
            input = input_;
        }

        public ElementManipulation(ElementType element_, InputState input_)
        {
            elementType = element_;
            input = input_;
        }

        public void Update(CommonComponentsHolder commonComponents)
        {
            InputBank inputBank = commonComponents.InputBank;

            if (input.JustReleased)
            {
                if (currentMass != null)
                {
                    if (inputBank.LatestGesture != null)
                    {
                        inputBank.ResetGestures();
                        BaseElementMassState newState = elementType.CreateElementMassState(inputBank.LatestGesture, currentMass);
                        if (newState != null)
                        {
                            currentMass.ActiveStateMachine.setState(newState);
                        }
                    }
                    else
                    {
                        BaseElementMassState newState = elementType.CreateElementMassState(elementType.LetGoState, currentMass);
                        if (newState != null)
                        {
                            currentMass.ActiveStateMachine.setState(newState);
                        }
                    }
                }
            }

            if (input.Down)
            {
                if (currentMass == null || currentMass.Casted)
                {
                    currentMass = UnityEngine.Object.Instantiate(elementType.ElementPrefab);
                    currentMass.Init(elementType);
                    currentMass.ActiveStateMachine.CommonComponents = commonComponents;
                    currentMass.ActiveStateMachine.setState(elementType.CreateElementMassState(elementType.SpawnState, currentMass));
                    currentMass.transform.position = inputBank.AimPoint;
                }

                currentMass.SetPosition(inputBank.AimPoint);
            }
        }

        public void FixedUpdate(InputBank inputBank)
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