using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEditorInternal;
using UnityEngine;

namespace SpellCasting
{
    public class InputState
    {
        public bool Down;
        public bool JustPressed;
        public bool JustReleased;

        internal void UpdateInput(bool inputDown)
        {
            if (Down != inputDown)
            {
                JustPressed = inputDown;
                JustReleased = !inputDown;
            }
            else
            {
                JustPressed = false;
                JustReleased = false;
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

        public InputState M1 { get; set; } = new InputState();
        public InputState M2 { get; set; } = new InputState();
        public InputState Space { get; set; } = new InputState();
        public InputState Shift { get; set; } = new InputState();
         
        public Vector3 LocalMoveDirection { get; set; }
        public Vector3 GlobalMoveDirection { get; set; }

        public Vector3 GesturePosition { get; set; }
        public Vector3 GestureDelta { get; set; }
        private List<AimGesture> _gestures => GestureCatalog.AllGestures;
        private int _latestGesture;
        public AimGesture LatestGesture => _latestGesture >= 0 ? _gestures[_latestGesture] : null;

        [Header("Debug")]
        public float DebugSwipeMag;
        public float DebugSwirlTotalAngleSwirled;
        public float DebugShakeTurns;
        public string DebugCurrentGesture;

        void Awake()
        {
            _gestures.Sort();
        }

        public void ManageInputStates(InputState state, bool down, bool justPressed, bool justReleased)
        {
            state.Down = down;
            state.JustPressed = justPressed;
            state.JustReleased = justReleased;
        }

        void Update()
        {
            _latestGesture = -1;
            for (int i = 0; i < _gestures.Count; i++)
            {
                AimGesture gesture = _gestures[i];

                bool qualified = gesture.QualifyGesture(this);
                if (_latestGesture == -1 && qualified)
                {
                    _latestGesture = i;
                }
            }

            DebugCurrentGesture = LatestGesture != null ? LatestGesture.ToString() : "none";
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