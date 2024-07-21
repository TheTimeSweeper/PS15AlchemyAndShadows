using ActiveStates.Elements;
using UnityEngine;


namespace ActiveStates
{
    public abstract class ActiveState
    {
        public ActiveStateMachine machine;

        private float _fixedAge;
        protected float fixedAge => _fixedAge;

        protected virtual void EndState()
        {
            machine.endState(this);
        }
        public virtual void OnFixedUpdate()
        {
            _fixedAge += Time.fixedDeltaTime;

        }
        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnExit() { }
    }
}
