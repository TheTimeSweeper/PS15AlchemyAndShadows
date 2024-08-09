using SpellCasting;
using System;
using UnityEngine;

namespace ActiveStates
{
    public abstract class ActiveState
    {
        public ActiveStateMachine Machine;
        protected GameObject gameObject => Machine.gameObject;
        protected Transform transform => Machine.transform;
        protected CommonComponentsHolder components => Machine.CommonComponents;

        protected CharacterBody characterBody => Machine.CommonComponents.CharacterBody;
        protected InputBank inputBank => Machine.CommonComponents.InputBank;
        protected HealthComponent healthComponent => Machine.CommonComponents.HealthComponent;
        protected Caster caster => Machine.CommonComponents.Caster;
        protected ManaComponent manaComponent => Machine.CommonComponents.ManaComponent;
        protected FixedMotorDriver fixedMotorDriver => Machine.CommonComponents.FixedMotorDriver;
        protected CharacterModel characterModel => Machine.CommonComponents.CharacterModel;
        protected StateMachineLocator stateMachineLocator => Machine.CommonComponents.StateMachineLocator;
        protected TeamComponent teamComponent => Machine.CommonComponents.TeamComponent;
        //jam should be on characterbody
        protected Animator animator => Machine.CommonComponents.Animator;

        private float _fixedAge;
        protected float fixedAge => _fixedAge;

        protected void EndState()
        {
            Machine.EndState(this);
        }

        public virtual void OnFixedUpdate()
        {
            _fixedAge += Time.fixedDeltaTime;
        }
        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnExit(bool machineDed = false) { }
        public virtual void ModifyNextState(ActiveState state) { }

        public virtual InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.STATE_ANY;
        }

        public virtual ActiveState Clone(){ return ActiveStateCatalog.InstantiateState(this.GetType()); }
    }
}
