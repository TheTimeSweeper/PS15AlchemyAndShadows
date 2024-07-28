﻿using SpellCasting;
using SpellCasting.AI;
using UnityEngine;

namespace ActiveStates.AI
{
    public class ChaseToCombat : AITargetState
    {
        public float ChaseTime = 1;

        public override void OnEnter()
        {
            base.OnEnter();
            Gesture = Brain.RollGesture();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (Brain.CurrentTargetBody == null)
            {
                machine.setStateToDefault();
                return;
            }

            Vector3 difference = Brain.CurrentTargetBody.transform.position - transform.position;

            if (difference.magnitude >= Gesture.CloseDistasnce)
            {
                Brain.AIInputController.MoveDirection = -difference;
            } 
            else
            {
                Brain.AIInputController.MoveDirection = Vector3.zero;

                if (fixedAge > ChaseTime)
                {
                    machine.setState(new Combat { Brain = Brain, Gesture = Gesture });
                }
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            Brain.AIInputController.MoveDirection = Vector3.zero;
        }
    }
}
