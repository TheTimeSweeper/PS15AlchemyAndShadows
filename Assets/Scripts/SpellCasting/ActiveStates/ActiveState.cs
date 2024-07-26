using SpellCasting;
using System;
using UnityEngine;

namespace ActiveStates
{
    public abstract class ActiveState
    {
        public ActiveStateMachine machine;
        protected GameObject gameObject => machine.gameObject;
        protected Transform transform => machine.transform;
        protected CommonComponentsHolder components => machine.CommonComponents;

        protected CharacterBody characterBody => machine.CommonComponents.CharacterBody;
        protected InputBank inputBank => machine.CommonComponents.InputBank;
        protected HealthComponent healthComponent => machine.CommonComponents.HealthComponent;
        protected Caster caster => machine.CommonComponents.Caster;
        protected FixedMotorDriver fixedMotorDriver => machine.CommonComponents.FixedMotorDriver;
        protected CharacterModel characterModel => machine.CommonComponents.CharacterModel;
        protected StateMachineLocator stateMachineLocator => machine.CommonComponents.StateMachineLocator;
        protected TeamComponent teamComponent => machine.CommonComponents.TeamComponent;

        private float _fixedAge;
        protected float fixedAge => _fixedAge;

        protected virtual void EndState()
        {
            machine.endState(this);
        }
        public virtual void OnFixedUpdate()
        {
            _fixedAge += Time.fixedDeltaTime;
        }

        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnExit() { }
        public virtual void ModifyNextState(ActiveState state) { }

        public virtual InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.STATE_ANY;
        }
    }
}
