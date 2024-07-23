using SpellCasting;
using System.Diagnostics;
using UnityEngine;

namespace ActiveStates
{
    public abstract class BasicMeleeAttack : BasicTimedState
    {
        protected abstract string hitboxName { get; }
        protected abstract float damageCoefficient { get; }

        protected OverlapAttack attack;

        public override void OnEnter()
        {
            base.OnEnter();

            //attack = new OverlapAttack
            //{
            //    Damage = damageCoefficient * characterBody.stats.Damage,
            //    Hitbox = hitboxLocator.LocateByName(hitboxName),
            //    Owner = gameObject
            //};
        }
    }
}
