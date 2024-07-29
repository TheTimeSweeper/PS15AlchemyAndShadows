using UnityEngine;

namespace SpellCasting.Projectiles
{
    public class ProjectileSpawnChildren : MonoBehaviour, IProjectileSubComponent
    {
        [SerializeField]
        private ProjectileController childProjectile;

        [SerializeField]
        private float interval = 0.3f;

        float _tim;

        public FireProjectileInfo ProjectileInfo { get; set; }

        void FixedUpdate ()
        {
            _tim -= Time.deltaTime;

            if (_tim <= 0)
            {
                _tim = interval;

                ProjectileController chidlProjecilte = Object.Instantiate(childProjectile, transform.position, Quaternion.identity);
                chidlProjecilte.Init(new FireProjectileInfo
                {
                    OwnerObject = ProjectileInfo.OwnerObject,
                    OwnerBody = ProjectileInfo.OwnerBody,
                    Damage = ProjectileInfo.Damage,
                    StartPosition = transform.position,
                    TeamIndex = ProjectileInfo.TeamIndex,
                    DamageType = ProjectileInfo.DamageType
                });
            }
        }
    }
}