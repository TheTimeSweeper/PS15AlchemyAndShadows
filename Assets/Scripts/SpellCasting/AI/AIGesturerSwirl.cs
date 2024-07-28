using UnityEngine;

namespace SpellCasting.AI
{
    [CreateAssetMenu(menuName = "AIGesturer/Swirl", fileName = "AIGestureSwirl")]
    public class AIGesturerSwirl : AIGesture
    {
        [SerializeField]
        private float offsetDistance;

        [SerializeField]
        private float angularVelocity;

        public override ScriptableObjectBehavior GetBehavior()
        {
            return new AISwirlBehavior { infoObject = this };
        }

        public class AISwirlBehavior : AIGestureBehavior<AIGesturerSwirl>
        {
            private Vector3 initialPosition;
            private Vector3 offest = Vector3.forward;
            private float angle;
            private float velocity;

            public override bool OnFixedUpdate(AIBrain brain)
            {
                bool end = base.OnFixedUpdate(brain);

                if (initialPosition == Vector3.zero)
                {
                    initialPosition = brain.CurrentTargetPosition;
                }

                brain.AIInputController.downInputs[InfoObject.inputIndex] = true;

                Quaternion quaternion = Quaternion.Euler(0, InfoObject.angularVelocity, 0);
                offest = quaternion * offest;

                brain.AIInputController.CurrentAimPosition = initialPosition + offest * InfoObject.offsetDistance;

                return end;
            }
        }
    }
}
