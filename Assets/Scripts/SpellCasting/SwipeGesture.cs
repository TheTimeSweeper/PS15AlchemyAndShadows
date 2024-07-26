using UnityEngine;

namespace SpellCasting
{
    [CreateAssetMenu(menuName = "GestureType/Swipe", fileName = "GestureSwipe")]
    public class SwipeGesture : AimGesture
    {
        [SerializeField]
        private float threshold = 0.2f;

        public override bool QualifyGesture(InputBank bank, InputState inputState)
        {
            bank.DebugSwipeMag = bank.GestureDistance;
            return bank.GestureDistance > threshold;
        }
    }
}