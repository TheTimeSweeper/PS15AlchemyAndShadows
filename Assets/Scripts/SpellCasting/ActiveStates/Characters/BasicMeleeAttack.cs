﻿using SpellCasting;
using System.Diagnostics;
using UnityEngine;

namespace ActiveStates.Characters
{
    public abstract class BasicMeleeAttack : BasicTimedState
    {
        protected abstract string hitboxName { get; }
        protected abstract float damageCoefficient { get; }
        protected virtual float positionShift => 1;
        protected virtual float positionShiftDeceleration => 16;

        protected OverlapAttack attack;

        private Vector3 _shift;

        public Vector3 aimDirection;

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

            _shift = inputBank.GlobalMoveDirection * gameObject.GetTestValue(0) * characterBody.stats.MoveSpeed;

            if (aimDirection == default)
            {
                aimDirection = inputBank.GlobalMoveDirection;
            }
            characterModel.CharacterDirection.OverrideLookDirection(aimDirection, duration);

            //jam effectcatalog when
            if (characterModel.particleSystemLocator)
            {
                ParticleSystem swipeParticle = characterModel.particleSystemLocator.LocateByName("SwipeParticle");
                if (swipeParticle) 
                {
                    swipeParticle.Play(); 
                }
            }
        }

        protected override void OnCastFixedUpdate()
        {
            base.OnCastFixedUpdate();

            fixedMotorDriver.AddedMotion = _shift;
            _shift = Util.ExpDecayLerp(_shift, default, gameObject.GetTestValue(1), Time.fixedDeltaTime);

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
