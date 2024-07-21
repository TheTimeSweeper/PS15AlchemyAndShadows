using ActiveStates;
using ActiveStates.Elements;

public class ElementManipulation
{
    private ElementType elementType;
    private InputState input;

    private ElementMass currentMass;

    public ElementManipulation(ElementTypeIndex element_, InputState input_)
    {
        elementType = ElementCatalog.ElementTypes[element_];
        input = input_;
    }

    public void Update(InputBank inputBank)
    {
        if (input.JustReleased)
        {
            if (currentMass != null)
            {
                if(inputBank.LatestGesture != null)
                {
                    inputBank.ResetGestures();
                    BaseElementMassState newState = elementType.CreateElementMassState(inputBank.LatestGesture, currentMass, inputBank);
                    if(newState != null)
                    {
                        currentMass.ActiveStateMachine.setState(newState);
                    }
                } 
                else
                {
                    BaseElementMassState newState = elementType.CreateElementMassState(elementType.LetGoState, currentMass, inputBank);
                    if (newState != null)
                    {
                        currentMass.ActiveStateMachine.setState(newState);
                    }
                }
            }
        }
    }

    public void FixedUpdate(InputBank inputBank)
    {
        if (input.Down)
        {
            if (currentMass == null || currentMass.Casted)
            {
                currentMass = UnityEngine.Object.Instantiate(elementType.ElementPrefab);
                currentMass.Init(elementType);

                currentMass.ActiveStateMachine.setState(elementType.CreateElementMassState(elementType.SpawnState, currentMass, inputBank));
                currentMass.transform.position = inputBank.AimPoint;
            }

            currentMass.SetPosition(inputBank.AimPoint);
        }
    }
}
