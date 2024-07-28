namespace ActiveStates.AI
{
    public class Combat : AITargetState
    {
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (Brain.CurrentGesture != null)
            {
                Brain.CurrentGesture.OnFixedUpdate(Brain);
            } 
            else
            {
                machine.setStateToDefault();
            }
        }
    }
}
