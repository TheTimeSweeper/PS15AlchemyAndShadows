using UnityEngine;

namespace ActiveStates.Characters
{
    public class Knockback : BasicTimedStateSimple
    {
        protected override float baseDuration => 0.5f;

        private Vector3 _shift;

        public override void OnEnter()
        {
            base.OnEnter();

            _shift = inputBank.GlobalMoveDirection * gameObject.GetTestValue(1) * characterBody.stats.MoveSpeed;

        }
    }

    public class StunnedState : BasicTimedStateSimple
    {
        public float StunTime;

        public GameObject EffectPrefab;
        private GameObject _effectObject;

        protected override float baseDuration => StunTime;

        public override void OnEnter()
        {
            base.OnEnter();

            if (EffectPrefab != null)
            {
                _effectObject = Object.Instantiate(EffectPrefab, transform.position, Quaternion.identity, transform);
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            if (_effectObject != null)
            {
                Object.Destroy(_effectObject);
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.STUN;
        }
    }
}