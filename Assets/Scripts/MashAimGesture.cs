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

        private int _mashes;
        private float _resetTimer;

        public override bool QualifyGesture(InputBank bank)
        {
            _resetTimer += Time.deltaTime;
            if(_resetTimer > resetTime)
            {
                _mashes = 0;
            }

            if (bank.M1.JustPressed)
            {
                _mashes++;
                _resetTimer = 0;
            }
            return _mashes >= requiredMashes;
        }

        public override void ResetGesture()
        {
            _mashes = 0;
        }
    }
}