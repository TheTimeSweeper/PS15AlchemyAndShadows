using UnityEngine;

namespace ActiveStates
{
    public class GenericMeleeCombo : BasicMeleeAttack
    {
        protected override string hitboxName => "Sword";
        protected override float damageCoefficient => 1;
        protected override float BaseDuration => 1;
        protected override float BaseCastStartTimeFraction => 0.1f;
        protected override float BaseCastEndTimeFraction => 0.4f;
        protected override float BaseInterruptibleTimeFraction => 0.6f;

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.LogWarning("entering melee");

        }

        protected override void OnCastEnter()
        {
            base.OnCastEnter();
            Debug.LogWarning("doing melee and it is very cool");
        }
    }
}
