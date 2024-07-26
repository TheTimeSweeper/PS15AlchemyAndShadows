using System;
using UnityEngine;

namespace SpellCasting
{
    [CreateAssetMenu(menuName = "GestureType/Mash", fileName = "GestureMash")]
    public class MashAimGesture : AimGesture
    {
        [SerializeField]
        private int requiredMashes;

        [SerializeField]
        private float resetTime = 1;

        [SerializeField]
        private float _movedThreshold;

        private int _mashes;
        private float _nextResetTime;

        private Vector3 _initialInput;

        public override bool QualifyGesture(InputBank bank, InputState inputState)
        {
            if(Time.time > _nextResetTime)
            {
                _mashes = 0;
                _initialInput = default;
            }

            if (inputState.JustPressed(this))
            {
                _mashes++;
                _nextResetTime = Time.time + resetTime;
                if (_initialInput == default)
                {
                    _initialInput = bank.GesturePosition;
                }
            }

            return _mashes >= requiredMashes && (bank.GesturePosition - _initialInput).magnitude < _movedThreshold;
        }

        public override void ResetGesture()
        {
            _mashes = 0;
            _initialInput = default;
        }
    }
}