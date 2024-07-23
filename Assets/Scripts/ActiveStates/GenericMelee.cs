using UnityEngine;

namespace ActiveStates
{
    public class GenericMelee : BasicMeleeAttack
    {
        protected override string hitboxName => "Sword";
        protected override float damageCoefficient => 1;
        protected override float TimedBaseDuration => 1;
        protected override float TimedBaseCastStartPercentTime => 0.2f;

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
