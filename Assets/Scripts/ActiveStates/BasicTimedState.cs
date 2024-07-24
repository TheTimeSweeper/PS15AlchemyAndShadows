namespace ActiveStates
{
    public abstract class BasicTimedState : ActiveState
    {
        //total duration of the move
        protected abstract float BaseDuration { get; }

        //0-1 time relative to duration that the skill starts
        //for example, set 0.5 and the "cast" will happen halfway through the skill
        protected abstract float BaseCastStartTimeFraction { get; }
        protected virtual float BaseCastEndTimeFraction => 1;
        protected virtual float BaseInterruptibleTimeFraction => 1;

        protected float duration;
        protected float castStartTime;
        protected float castEndTime;
        protected bool hasFired;
        protected bool isFiring;
        protected bool hasExited;

        public override void OnEnter()
        {
            InitDurationValues();
            base.OnEnter();
        }

        protected virtual void InitDurationValues()
        {
            duration = BaseDuration / characterBody.stats.Damage;
            this.castStartTime = BaseCastStartTimeFraction * duration;
            this.castEndTime = BaseCastEndTimeFraction * duration;
        }

        protected virtual void OnCastEnter() { }
        protected virtual void OnCastFixedUpdate() { }
        protected virtual void OnCastUpdate() { }
        protected virtual void OnCastExit() { }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            bool fireStarted = fixedAge >= castStartTime;
            bool fireEnded = fixedAge >= castEndTime;
            isFiring = false;

            //to guarantee attack comes out if at high attack speed the fixedage skips past the endtime
            if ((fireStarted && !fireEnded) || (fireStarted && fireEnded && !this.hasFired))
            {
                isFiring = true;
                OnCastFixedUpdate();
                if (!hasFired)
                {
                    OnCastEnter();
                    hasFired = true;
                }
            }

            if (fireEnded && !hasExited)
            {
                hasExited = true;
                OnCastExit();
            }

            if (fixedAge > duration)
            {
                SetNextState();
                return;
            }
        }

        protected virtual void SetNextState()
        {
            machine.setStateToDefault();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (isFiring)
            {
                OnCastUpdate();
            }
        }
    }
}
