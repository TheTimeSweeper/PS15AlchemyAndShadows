namespace ActiveStates.Characters
{
    public class GenericCharacterMove : ActiveState
    {
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            fixedMotorDriver.Direction = inputBank.GlobalMoveDirection;
            fixedMotorDriver.DesiredSpeed = characterBody.stats.MoveSpeed;
            characterModel.CharacterDirection.DesiredDirection = fixedMotorDriver.Direction;
        }

        public override void OnExit(bool machineDed = false)
        {
            base.OnExit(machineDed);
            fixedMotorDriver.Direction = UnityEngine.Vector3.zero;
        }
    }
}
