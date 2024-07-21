using ActiveStates;
using ActiveStates.Elements;
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ElementActionState
{
    [SerializeField]
    private AimGesture gestureType;
    public AimGesture GestureType => gestureType;

    [SerializeField]
    private SerializableActiveState gestureState;
    public SerializableActiveState GestureState => gestureState;
}

[CreateAssetMenu(menuName = "ElementType", fileName = "Element")]
public class ElementType : ScriptableObject
{
    [SerializeField]
    private ElementTypeIndex index;
    public ElementTypeIndex Index { get => index; }

    [SerializeField]
    private ElementMass elementPrefab;
    public ElementMass ElementPrefab { get => elementPrefab; }

    [SerializeField]
    private float growthMultiplier;
    public float GrowthMultiplier { get => growthMultiplier; }

    [SerializeField]
    private float baseMass;
    public float BaseMass { get => baseMass; }

    [SerializeField]
    private float velocityMultiplier;
    public float VelocityMultiplier { get => velocityMultiplier; }

    [SerializeField]
    private SerializableActiveState spawnState = new SerializableActiveState(typeof(BasicElementGrow));
    public SerializableActiveState SpawnState => spawnState;
    [SerializeField]
    private SerializableActiveState letGoState= new SerializableActiveState(typeof(BasicElementLetGo));
    public SerializableActiveState LetGoState => letGoState;

    [SerializeField]
    private List<ElementActionState> elementActions;

    public SerializableActiveState FindGestureStateType(AimGesture gesture)
    {
        ElementActionState elementActionState = elementActions.Find((action) =>
                {
                    return action.GestureType == gesture;
                });
        return elementActionState != null? elementActionState.GestureState : default;
    }

    public BaseElementMassState CreateElementMassState(AimGesture latestGesture, ElementMass elementMass, InputBank inputBank)
    {
        SerializableActiveState state = FindGestureStateType(latestGesture);
        if(string.IsNullOrEmpty(state.activeStateName))
        {
            return null;
        }
        return CreateElementMassState(state, elementMass, inputBank);
    }
    public BaseElementMassState CreateElementMassState(SerializableActiveState state, ElementMass elementMass, InputBank inputBank)
    {
        BaseElementMassState newState = ActiveStateCatalog.InstantiateState<BaseElementMassState>(state);
        newState.Init(elementMass, this, inputBank);
        return newState;
    }
}
