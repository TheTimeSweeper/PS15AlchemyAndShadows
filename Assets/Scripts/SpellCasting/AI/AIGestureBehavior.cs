using UnityEngine;

namespace SpellCasting.AI
{
    public abstract class AIGestureBehavior : ScriptableObjectBehavior
    {
        protected float fixedAge;

        public abstract float CloseDistasnce { get; }

        public abstract void OnFixedUpdate(AIBrain brain);

        public abstract void End(AIBrain brain);
    }

    public abstract class AIGestureBehavior<T> : AIGestureBehavior where T : AIGesture
    {
        private T _infoObject;
        public T InfoObject
        {
            get
            {
                if (_infoObject == null)
                {
                    _infoObject = base.infoObject as T;
                }
                return _infoObject;
            }
        }
        public override float CloseDistasnce { get => InfoObject.CloseDistance; }

        public override void OnFixedUpdate(AIBrain brain)
        {
            fixedAge += Time.fixedDeltaTime;

            if (fixedAge > InfoObject.duration)
            {
                End(brain);
            }
        }

        public override void End(AIBrain brain)
        {
            brain.AIInputController.downInputs[InfoObject.inputIndex] = false;
            brain.CurrentGesture = null;
        }
    }
}
