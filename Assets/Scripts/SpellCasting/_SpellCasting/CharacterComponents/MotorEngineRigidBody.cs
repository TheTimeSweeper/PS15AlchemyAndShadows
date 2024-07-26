using UnityEngine;

namespace SpellCasting
{
    public class MotorEngineRigidBody : MotorEngine
    {
        [SerializeField]
        private Rigidbody rigidBody;

        public override void FixedMove(Vector3 movement)
        {
            rigidBody.MovePosition(transform.position + movement);
        }
    }
}
