using ActiveStates;
using System.Collections.Generic;
using UnityEngine;

namespace SpellCasting
{
    public class StateMachineLocator : ComponentLocator<ActiveStateMachine>
    {
        [SerializeField]
        private ActiveStateMachine mainStateMachine;
        public ActiveStateMachine MainStateMachine => mainStateMachine;


        public void SetStates(ActiveState state, bool main = true, bool others = true) => SetStates(state, InterruptPriority.HITSTUN, main, others);
        public void SetStates(ActiveState state, InterruptPriority priority, bool main = true, bool others = true)
        {
            if (main)
            {
                mainStateMachine.tryInterruptState(state, priority);
            }
            if (others)
            {
                for (int i = 0; i < ComponentList.Length; i++)
                {
                    ComponentList[i].tryInterruptState(state, priority);
                }
            }
        }
    }
}
