using UnityEngine;

namespace SpellCasting.Projectiles
{
    public class ProjectileDetachAndLerp : MonoBehaviour, IProjectileDormant, IProjectileSubComponent
    {
        [SerializeField]
        private Transform lerpedView;

        [SerializeField]
        private float time = 0.3f;

        [SerializeField]
        private AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        private float _lerpTim;

        private Vector3 _originalPosition;

        public FireProjectileInfo ProjectileInfo { get; set; }

        public void ProjectileWake()
        {
            lerpedView.transform.parent = null;
        }

        void Update()
        {
            _lerpTim += Time.deltaTime;
            lerpedView.transform.position = Vector3.Lerp(ProjectileInfo.PreviousPosition, transform.position, curve.Evaluate(_lerpTim/ time));
        }

        void OnDestroy()
        {
            Destroy(lerpedView.gameObject);
        }
    }
}