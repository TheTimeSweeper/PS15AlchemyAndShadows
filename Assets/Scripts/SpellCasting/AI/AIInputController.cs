using UnityEngine;
using UnityEngine.Windows;

namespace SpellCasting
{
    public class AIInputController : InputController
    {
        public bool[] downInputs = new bool[4];
        public int[] inputDownFrames = new int[4];

        public Vector3 MoveDirection { get; set; }
        public Vector3 CurrentAimPosition { get; set; }
        public Vector3 OverrideGesturePosition { get; set; }

        [SerializeField]
        private float gestureSpeedMultiplier;

        [SerializeField]
        public float DebugFixedAge;

        private void FixedUpdate()
        {
            for (int i = 0; i < inputDownFrames[i]; i++)
            {
                inputDownFrames[i]--;
                if(inputDownFrames[i] == 0)
                {
                    downInputs[i] = false;
                }
            }
        }

        public void JustPress(int input)
        {
            downInputs[input] = true;
            inputDownFrames[input] = 3;
        }

        protected override Vector3 GetAimPosition()
        {
            return CurrentAimPosition;
        }

        protected override Vector3 GetGesturePosition()
        {
            Vector3 gesture;
            if(OverrideGesturePosition != Vector3.zero)
            {
                gesture = OverrideGesturePosition;
            } 
            else
            {
                gesture = transform.TransformPoint(CurrentAimPosition) * gestureSpeedMultiplier;
            }
            return new Vector3(gesture.x, gesture.z, 0) * gestureSpeedMultiplier;
        }

        protected override Vector3 GetMovementInput()
        {
            return MoveDirection;
        }

        protected override void SetbuttonInputs()
        {
            inputBank.M1.UpdateInput(downInputs[0]);
            inputBank.M2.UpdateInput(downInputs[1]);
            inputBank.Shift.UpdateInput(downInputs[2]);
            inputBank.Space.UpdateInput(downInputs[3]);
        }
    }
}