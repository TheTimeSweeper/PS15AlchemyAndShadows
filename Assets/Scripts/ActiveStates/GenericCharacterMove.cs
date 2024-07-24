namespace ActiveStates
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

        public override void OnExit()
        {
            base.OnExit();
            fixedMotorDriver.Direction = UnityEngine.Vector3.zero;
        }
    }
}
