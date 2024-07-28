using SpellCasting;
using SpellCasting.AI;

namespace ActiveStates.AI
{
    public class Combat : AITargetState
    {
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (Brain.CurrentTargetBody == null)
            {
                machine.setStateToDefault();
                return;
            }

            bool ended = Gesture.OnFixedUpdate(Brain);

            if (ended)
            {
                if (Brain.CurrentTargetBody != null)
                {
                    machine.setState( new ChaseToCombat { Brain = Brain, ChaseTime = 0.5f } );
                }
                else
                {
                    machine.setStateToDefault();
                }
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            Gesture.End(Brain);
        }
    }
}
