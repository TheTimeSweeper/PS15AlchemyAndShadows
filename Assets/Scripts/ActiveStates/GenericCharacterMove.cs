namespace ActiveStates
{
    public class GenericCharacterMove : ActiveState
    {
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            fixedMotorDriver.Direction = inputBank.GlobalMoveDirection;
            fixedMotorDriver.DesiredSpeed = characterBody.stats.MoveSpeed;
        }
    }
}
