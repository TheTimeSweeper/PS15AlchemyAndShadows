using ActiveStates;
using ActiveStates.Elements;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpellCasting
{
    [System.Serializable]
    public class ElementActionState
    {
        [SerializeField]
        private AimGesture gestureType;
        public AimGesture GestureType => gestureType;

        [SerializeField]
        private SerializableActiveState gestureState;
        public SerializableActiveState GestureState => gestureState;

        [SerializeField]
        private float baseManaCost;
        public float BaseManaCost => baseManaCost;
    }

    [CreateAssetMenu(menuName = "ElementType/Unspecified", fileName = "Element")]
    public class ElementType : ScriptableObject
    {
        [SerializeField]
        private ElementTypeIndex index;
        public ElementTypeIndex Index { get => index; }

        [SerializeField]
        private ElementMass elementMassPrefab;
        public ElementMass ElementMassPrefab { get => elementMassPrefab; }

        [SerializeField]
        private CursorIndicator cursorIndicatorPrefab;
        public CursorIndicator CursorIndicatorPrefab { get => cursorIndicatorPrefab; set => cursorIndicatorPrefab = value; }

        [SerializeField]
        private Color elementColor;
        public Color ElementColor { get => elementColor; }

        [SerializeField]
        private float massGrowthMultiplier;
        public float MassGrowthMultiplier { get => massGrowthMultiplier; }

        [SerializeField]
        private float massBaseMass;
        public float BaseMass { get => massBaseMass; }

        [SerializeField]
        private float massVelocityMultiplier;
        public float MassVelocityMultiplier { get => massVelocityMultiplier; }

        [SerializeField]
        private SerializableActiveState spawnState = new SerializableActiveState(typeof(BasicElementGrow));
        public SerializableActiveState SpawnState => spawnState;
        [SerializeField]
        private SerializableActiveState letGoState = new SerializableActiveState(typeof(BasicElementLetGo));
        public SerializableActiveState LetGoState => letGoState;

        [SerializeField]
        private List<ElementActionState> elementActions;

        public SerializableActiveState FindGestureStateType(AimGesture gesture)
        {
            ElementActionState elementActionState = elementActions.Find((action) =>
                    {
                        return action.GestureType == gesture;
                    });
            return elementActionState != null ? elementActionState.GestureState : default;
        }

        public BaseElementMassState CreateElementMassState(AimGesture latestGesture, ElementMass elementMass)
        {
            SerializableActiveState state = FindGestureStateType(latestGesture);
            if (string.IsNullOrEmpty(state.activeStateName))
            {
                return null;
            }
            return CreateElementMassState(state, elementMass);
        }
        public BaseElementMassState CreateElementMassState(SerializableActiveState state, ElementMass elementMass)
        {
            BaseElementMassState newState = ActiveStateCatalog.InstantiateState<BaseElementMassState>(state);
            newState.Init(elementMass, this);
            return newState;
        }
    }
}