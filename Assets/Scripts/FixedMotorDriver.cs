using Unity.VisualScripting;
using UnityEngine;

namespace SpellCasting
{

    public class FixedMotorDriver : MonoBehaviour
    {
        [SerializeField]
        private MotorEngine engine;

        [SerializeField]
        private float acceleration;

        public Vector3 Direction { get; set; }
        public float DesiredSpeed { get; set; }
        public Vector3 AddedMotion { get; set; }
        public Vector3? OverrideVelocity { get; set; }

        private Vector3 _deltaPosition;
        private Vector3 _currentVelocity;
        private Vector3 _desiredVelocity;

        private Vector3 _lastPosition;

        void FixedUpdate()
        {
            _deltaPosition = transform.position - _lastPosition;

            _desiredVelocity = Direction * DesiredSpeed;
            _currentVelocity = Util.ExpDecayLerp(_currentVelocity, _desiredVelocity, acceleration, Time.fixedDeltaTime);

            //jam faster deceleration value for fuckin uhh stopping and moving opposite direction

            Vector3 movement = _currentVelocity;

            if (OverrideVelocity.HasValue)
            {
                movement = OverrideVelocity.Value;
                OverrideVelocity = null;
            }

            if (AddedMotion != default)
            {
                movement += AddedMotion;
                AddedMotion = default;
            }

            engine.FixedMove(movement);

            _lastPosition = transform.position;
        }
    }
}
