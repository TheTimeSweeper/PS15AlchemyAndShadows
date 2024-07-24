
using SpellCasting;
using System.Collections.Generic;
using UnityEngine;

namespace ActiveStates
{
    public class ActiveStateMachine : MonoBehaviour, IHasCommonComponents
    {
        [SerializeField]
        private string label;
        public string Label => label;

        [SerializeField]
        private CommonComponentsHolder commonComponents;
        public CommonComponentsHolder CommonComponents { get => commonComponents; set { commonComponents = value; } }

        [SerializeField]
        private SerializableActiveState DefaultState;

        private Queue<ActiveState> _queuedStates = new Queue<ActiveState>();
        private ActiveState _currentlyRunningState;

        private void Start()
        {
            if (_currentlyRunningState != null)
                return;

            _currentlyRunningState = new IdleState();

            if (string.IsNullOrEmpty(DefaultState.activeStateName))
                return;

            var defaultState = ActiveStateCatalog.InstantiateState(DefaultState);
            if (defaultState != null)
            {
                setState(ActiveStateCatalog.InstantiateState(DefaultState));
            }
        }

        void FixedUpdate()
        {
            _currentlyRunningState.OnFixedUpdate();
        }
        void Update()
        {
            _currentlyRunningState.OnUpdate();
        }

        public void setState(ActiveState newState)
        {
            if (newState == null)
            {
                Debug.LogError("Tried to enter a null state", this);
            }

            exitCurrentState();
            _currentlyRunningState = newState;

            enterCurrentState();
        }

        private void exitCurrentState()
        {
            if (_currentlyRunningState != null)
            {
                _currentlyRunningState.OnExit();
            }
        }

        private void enterCurrentState()
        {
            _currentlyRunningState.machine = this;
            _currentlyRunningState.OnEnter();
        }

        public void setStateToDefault(bool clearQueue = false)
        {
            setState(ActiveStateCatalog.InstantiateState(DefaultState));
            if (clearQueue)
            {
                _queuedStates.Clear();
            }
        }
        public void endState(ActiveState state)
        {
            if (_currentlyRunningState != state)
            {
                Debug.LogError($"trying to end a state ({state.GetType()}) that is not the current state ({_currentlyRunningState.GetType()})");
                return;
            }
            endState();
        }
        public void endState()
        {
            if (_queuedStates.Count > 0)
            {
                setState(_queuedStates.Dequeue());
            }
            else
            {
                setStateToDefault();
            }
        }
        public void queueState(ActiveState newState)
        {
            _queuedStates.Enqueue(newState);
        }
    }
}
