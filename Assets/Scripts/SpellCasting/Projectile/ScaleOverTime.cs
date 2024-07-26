using UnityEngine;

namespace SpellCasting.Projectiles
{

    public class ScaleOverTime : MonoBehaviour
    {
        [SerializeField]
        private float time = 0.3f;

        [SerializeField]
        private AnimationCurve curve = AnimationCurve.EaseInOut(0,0,1,1);

        private float _lerpTim;
        private Vector3 _originalScale;

        private void Awake()
        {
            _originalScale = transform.localScale;
            transform.localScale = _originalScale * curve.Evaluate(0);
        }

        private void FixedUpdate()
        {
            _lerpTim += Time.deltaTime / time;

            transform.localScale = _originalScale * curve.Evaluate(_lerpTim);
        }
    }
}