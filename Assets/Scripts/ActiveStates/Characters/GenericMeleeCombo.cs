using UnityEngine;

namespace ActiveStates.Characters
{
    public class GenericMeleeCombo : BasicMeleeAttack
    {
        protected override string hitboxName => "BasicHitbox";
        protected override float damageCoefficient => 1;
        protected override float baseDuration => 0.5f;
        protected override float baseCastStartTimeFraction => 0.1f;
        protected override float baseCastEndTimeFraction => 0.4f;
        protected override float baseOtherStateInterruptTimeFraction => 0.5f;
        protected override float baseMovementInterruptTimeFraction => 0.6f;

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.LogWarning("entering melee");
        }

        protected override void OnCastFixedUpdate()
        {
            base.OnCastFixedUpdate();
            Debug.LogWarning("doing melee and it is very cool");
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.LogWarning("done attacking at " + fixedAge.ToString("0.00"));
        }

        protected override void OnHitEnemyAuthority()
        {
            base.OnHitEnemyAuthority();
            Debug.LogWarning("hit a guy");
        }
    }
}
