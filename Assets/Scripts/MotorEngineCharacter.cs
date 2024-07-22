using Unity.VisualScripting;
using UnityEngine;

namespace SpellCasting
{
    public class MotorEngineCharacter : MotorEngine
    {
        [SerializeField]
        private CharacterController characterController;

        [SerializeField]
        private float gravity = 9.8f;

        private float _ySpeed;

        private void FixedUpdate()
        {
            if (characterController.isGrounded)
            {
                _ySpeed = 0;
            } else
            {
                _ySpeed -= Time.fixedDeltaTime * gravity;
            }
        }

        public override void FixedMove(Vector3 movement)
        {
            characterController.Move(movement + Vector3.up * _ySpeed);
        }
    }
}
