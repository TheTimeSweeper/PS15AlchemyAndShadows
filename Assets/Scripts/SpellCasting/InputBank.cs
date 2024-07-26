using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace SpellCasting
{
    public class InputState
    {
        public bool Down;
        public bool JustPressed(object claimant)
        {
            bool success = Down && !pressedClaimants.Contains(claimant);
            if (success)
            {
                pressedClaimants.Add(claimant);
            }
            return success;
        }
        public bool JustReleased(object claimant)
        {
            bool success = !Down && !releasedClaimants.Contains(claimant);
            if (success)
            {
                releasedClaimants.Add(claimant);
            }
            return success;
        }

        public List<object> pressedClaimants = new List<object>();
        public List<object> releasedClaimants = new List<object>();

        public void UpdateInput(bool inputDown)
        {
            if (Down == inputDown)
                return;

            if (inputDown)
            {
                releasedClaimants.Clear();
            }
            else
            {
                pressedClaimants.Clear();
            }

            Down = inputDown;
        }
    }

    public class InputBank : MonoBehaviour
    {
        public Vector3 AimPoint { get; set; }
        public Vector3 AimDirection { get; set; }
        public Vector3 AimOut
        {
            get
            {
                Vector3 result = AimPoint - transform.position;
                result.y = 0;
                return result.normalized;
            }
        }

        public InputState M1 = new InputState();
        public InputState M2 = new InputState();
        public InputState Space = new InputState();
        public InputState Shift = new InputState();
        //public InputState[] extraInputStates;

        public List<InputState> OrderedHeldInputs { get; set; } = new List<InputState>();

        public InputState CurrentPrimaryInput
        {
            get
            {
                if (OrderedHeldInputs.Count > 0)
                {
                    return OrderedHeldInputs[0];
                }
                else
                {
                    return null;
                }
            }
        }

        public Vector3 GesturePosition { get; set; }
        public Vector3 GestureDelta { get; set; }
        public float GestureDistance => Vector3.Distance(GesturePosition, _gestureLerpPosition);
        private Vector3 _gestureLerpPosition;

        public Vector3 LocalMoveDirection { get; set; }
        public Vector3 GlobalMoveDirection { get; set; }

        private List<InputState> _allInputStates;

        private List<AimGesture> _gestures => GestureCatalog.AllGestures;
        private List<AimGesture> _qualifiedGestures = new List<AimGesture>();

        [Header("Debug")]
        public float DebugSwipeMag;
        public float DebugSwirlTotalAngleSwirled;
        public float DebugShakeTurns;
        public List<String> DebugCurrentGestures;
        public float GestureDistancee;

        void Awake()
        {
            _gestures.Sort();

            _allInputStates = new List<InputState> { M1, M2, Space, Shift }; 
            //AllInputStates.AddRange(extraInputStates);
        }

        void Update()
        {
            DebugCurrentGestures.Clear();

            _gestureLerpPosition = Util.ExpDecayLerp(_gestureLerpPosition, GesturePosition, 10, Time.deltaTime);
            GestureDistancee = GestureDistance;

            InputState iterState;
            for (int i = 0; i < _allInputStates.Count; i++)
            {
                iterState = _allInputStates[i];
                if ((iterState.Down) && !OrderedHeldInputs.Contains(iterState))
                {
                    OrderedHeldInputs.Add(iterState);
                }
            }

            for (int i = _allInputStates.Count - 1; i >= 0; i--)
            {
                iterState = _allInputStates[i];
                if (!iterState.Down && !iterState.JustReleased(this) && OrderedHeldInputs.Contains(iterState))
                {
                    OrderedHeldInputs.Remove(iterState);
                }
            }

            if (CurrentPrimaryInput != null)
            {
                for (int i = 0; i < _gestures.Count; i++)
                {
                    AimGesture gesture = _gestures[i];

                    bool qualified = gesture.QualifyGesture(this, CurrentPrimaryInput);

                    SetGestureQualified(gesture, qualified);

                    if (qualified)
                    {
                        DebugCurrentGestures.Add(gesture.ToString());
                    }
                }
            }
        }

        private void SetGestureQualified(AimGesture gesture, bool shouldAdd)
        {
            if (shouldAdd)
            {
                if (_qualifiedGestures.Contains(gesture))
                    return;
                _qualifiedGestures.Add(gesture);
            }
            else
            {
                for (int i = _qualifiedGestures.Count - 1; i >= 0; i--)
                {
                    if (_qualifiedGestures[i] == gesture)
                    {
                        _qualifiedGestures.RemoveAt(i);
                    }
                }
            }
            _qualifiedGestures.Sort();
        }

        public ElementActionState GetFirstQualifiedElementAction(List<ElementActionState> availableGestures)
        {
            for (int i = 0; i < _qualifiedGestures.Count; i++)
            {
                AimGesture gesture = _qualifiedGestures[i];
                ElementActionState actionState = availableGestures.Find((action) => { return action.GestureType == gesture; });
                if (actionState != null)
                {
                    return actionState;
                }
            }
            return null;
        }

        public void ResetGestures()
        {
            for (int i = 0; i < _gestures.Count; i++)
            {
                _gestures[i].ResetGesture();
            }
        }
    }
}