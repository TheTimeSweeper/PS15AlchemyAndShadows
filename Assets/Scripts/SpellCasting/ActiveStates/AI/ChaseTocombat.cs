using SpellCasting;
using SpellCasting.AI;
using UnityEngine;

namespace ActiveStates.AI
{
    public class ChaseTocombat : AITargetState
    {
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            Vector3 difference = TargetBody.transform.position - transform.position;

            Brain.AIInputController.MoveDirection = -difference;

            if (difference.magnitude < Brain.CurrentGesture.CloseDistasnce)
            {
                machine.setState(new Combat { TargetBody = TargetBody, Brain = Brain });
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            Brain.AIInputController.MoveDirection = Vector3.zero;
        }
    }
}
