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
            public override void OnFixedUpdate(AIBrain inputController)
            {
                base.OnFixedUpdate(inputController);

                if (initialPosition == Vector3.zero)
                {
                    initialPosition = inputController.CurrentTargetPosition;
                }

                inputController.AIInputController.CurrentAimPosition = inputController.CurrentTargetPosition;
                inputController.AIInputController.OverrideGesturePosition = initialPosition;
                inputController.AIInputController.downInputs[InfoObject.inputIndex] = true;
            }
        }
    }
}
