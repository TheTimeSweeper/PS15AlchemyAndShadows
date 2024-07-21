
using System.Collections.Generic;
using UnityEngine;

namespace ActiveStates
{
    public class ActiveStateMachine : MonoBehaviour
    {
        private Queue<ActiveState> _queuedStates = new Queue<ActiveState>();
        private ActiveState _currentlyRunningState;

        void Awake()
        {
            _currentlyRunningState = new IdleState();
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
            exitCurrentState();
            _currentlyRunningState = newState;
            enterCurrentState();
        }

        private void exitCurrentState()
        {
            _currentlyRunningState.OnExit();
        }

        private void enterCurrentState()
        {
            _currentlyRunningState.machine = this;
            _currentlyRunningState.OnEnter();
        }

        public void setStateToIdle(bool clearQueue = false)
        {
            setState(new IdleState());
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
                setStateToIdle();
            }
        }
        public void queueState(ActiveState newState)
        {
            _queuedStates.Enqueue(newState);
        }
    }
}
