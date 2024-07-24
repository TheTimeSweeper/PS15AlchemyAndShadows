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

            attack = new OverlapAttack
            {
                Damage = damageCoefficient * characterBody.stats.Damage,
                Hitbox = characterModel.HitboxLocator.LocateByName(hitboxName),
                OwnerGameObject = gameObject,
                Team = teamComponent.TeamIndex
            };
        }

        protected override void OnCastFixedUpdate()
        {
            base.OnCastFixedUpdate();

            if (attack.Fire())
            {
                OnHitEnemyAuthority();
            }
        }

        protected virtual void OnHitEnemyAuthority()
        {
            //Util.PlaySound(hitSoundString, gameObject);

            //if (!hasHopped)
            //{
            //    if (characterMotor && !characterMotor.isGrounded && hitHopVelocity > 0f)
            //    {
            //        SmallHop(characterMotor, hitHopVelocity);
            //    }

            //    hasHopped = true;
            //}

            //ApplyHitstop();
        }


    }
}
