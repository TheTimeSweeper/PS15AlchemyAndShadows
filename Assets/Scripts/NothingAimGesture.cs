using UnityEngine;

namespace SpellCasting
{
    [CreateAssetMenu(menuName = "GestureType/Nothing", fileName = "GestureNothing")]
    public class NothingAimGesture : AimGesture
    {
        public override bool QualifyGesture(InputBank bank, InputState inputState)
        {
            return bank.GestureDelta == default;
        }
    }
}