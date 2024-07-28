using UnityEngine;

namespace SpellCasting.AI
{
    [CreateAssetMenu(menuName = "AIGesturer/Click", fileName = "AIGestureClick")]
    public class AIGesturerClick : AIGesture
    {
        public override ScriptableObjectBehavior GetBehavior()
        {
            return new AIClickBehavior { infoObject = this };
        }

        public class AIClickBehavior : AIGestureBehavior<AIGesturerClick>
        {
            private Vector3 initialPosition;

            public override bool OnFixedUpdate(AIBrain brain)
            {
                bool end = base.OnFixedUpdate(brain);

                if (initialPosition == Vector3.zero)
                {
                    initialPosition = brain.CurrentTargetPosition;
                }

                brain.AIInputController.CurrentAimPosition = brain.CurrentTargetPosition;
                brain.AIInputController.OverrideGesturePosition = initialPosition;
                brain.AIInputController.downInputs[InfoObject.inputIndex] = true;

                return end;
            }
        }
    }
}
