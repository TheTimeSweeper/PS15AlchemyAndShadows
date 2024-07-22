using UnityEngine;

namespace SpellCasting
{
    [CreateAssetMenu(menuName = "GestureType/Swipe", fileName = "GestureSwipe")]
    public class SwipeGesture : AimGesture
    {
        [SerializeField]
        private float threshold = 0.2f;

        public override bool QualifyGesture(InputBank bank)
        {
            bank.DebugSwipeMag = bank.GestureDelta.magnitude;
            return bank.GestureDelta.magnitude > threshold;
        }
    }
}