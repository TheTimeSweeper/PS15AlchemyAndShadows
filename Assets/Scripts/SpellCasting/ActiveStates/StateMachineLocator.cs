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
    }
}
