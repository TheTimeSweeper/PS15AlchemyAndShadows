using SpellCasting.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpellCasting
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField]
        private CameraController cameraController;

        [SerializeField]
        private InputBank inputBank;

        [SerializeField]
        private float maxInputRange;

        private Vector3 aimOriginPosition => cameraController.AimOrigin.position;

        private Vector3 _currentAimPosition;
        private Vector3 _lastAimPosition;

        private Vector3 _lastUnrestrictedAimPosition;
        private Vector3 _currentAimDirection;

        private Vector3 _currentMousePosition;
        private Vector3 _lastMousePosition;
        private Vector3 _mouseDelta;

        private Vector3 _debugLastMousePosition;

        public List<Vector3> deltaEnds = new List<Vector3>();
        public List<Vector3> deltaStarts = new List<Vector3>();

        private int debugstep;

        void Awake()
        {
            _lastMousePosition = Input.mousePosition;

            _currentAimPosition = cameraController.GetAimPointFromMouse(Input.mousePosition);
            _lastAimPosition = _currentAimPosition;
        }

        void Update()
        {
            if (inputBank != null)
            {
                UpdateAimPoint();
                UpdateMouseDelta();
                inputBank.AimPoint = _currentAimPosition;
                inputBank.AimDirection = _currentAimDirection;
                inputBank.GestureDelta = _mouseDelta;
                inputBank.GesturePosition = _currentMousePosition;
                UpdateDebug();

                inputBank.M1.UpdateInput(Input.GetMouseButton(0));
                inputBank.M2.UpdateInput(Input.GetMouseButton(1));
                inputBank.Space.UpdateInput(Input.GetKey(KeyCode.Space));
                inputBank.Shift.UpdateInput(Input.GetKey(KeyCode.LeftShift));

                inputBank.LocalMoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                inputBank.GlobalMoveDirection = transform.TransformDirection(inputBank.LocalMoveDirection);
            }
        }

        private void UpdateMouseDelta()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.x /= Screen.width;
            mousePosition.y /= Screen.height;
            _currentMousePosition = mousePosition * 100;

            _mouseDelta = _currentMousePosition - _lastMousePosition;
            _lastMousePosition = _currentMousePosition;
        }

        private void UpdateAimPoint()
        {
            _currentAimPosition = cameraController.GetAimPointFromMouse(Input.mousePosition);
            _currentAimDirection = (_currentAimPosition - _lastAimPosition).normalized;

            _currentAimDirection = (_currentAimPosition - _lastUnrestrictedAimPosition).normalized;
            _lastUnrestrictedAimPosition = _currentAimPosition;

            //jam this should probably be in input bank or caster
            Vector3 aimDistance = _currentAimPosition - aimOriginPosition;
            if (Vector3.Magnitude(aimDistance) > maxInputRange)
            {
                _currentAimPosition = aimOriginPosition + aimDistance.normalized * maxInputRange;
            }
            AimMouseManager.AimMousePosition = cameraController.GetMousePointFromWorld(_currentAimPosition);

            _lastAimPosition = _currentAimPosition;
        }

        private void UpdateDebug()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                debugstep++;
            }
            if (debugstep < deltaStarts.Count && debugstep > 0)
            {
                Debug.DrawLine(Vector3.zero, deltaEnds[debugstep], Color.red);
                Debug.DrawLine(deltaStarts[debugstep], deltaEnds[debugstep], Color.red);
            }

            for (int i = 0; i < deltaStarts.Count; i++)
            {
                Debug.DrawLine(deltaEnds[i] - Vector3.up, deltaEnds[i], new Color(1, 1, 1, 0.3f));
                Debug.DrawLine(deltaStarts[i], deltaEnds[i], Color.cyan);
            }

            if (Input.GetKey(KeyCode.G))
            {
                deltaStarts.Add(_debugLastMousePosition);
                deltaEnds.Add(inputBank.GesturePosition);
            }

            if (Input.GetKey(KeyCode.H))
            {
                deltaStarts.Clear();
                deltaEnds.Clear();
                debugstep = -1;
            }

            _debugLastMousePosition = inputBank.GesturePosition;
        }
    }
}