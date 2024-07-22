using UnityEngine;

namespace SpellCasting.Projectiles
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField]
        private MonoBehaviour[] dormantComponents;

        public float BaseDamage { get; set; }
        //jam todo teamcomponent maybe
        public CharacterBody Owner { get; set; }

        void Awake()
        {
            for (int i = 0; i < dormantComponents.Length; i++)
            {
                dormantComponents[i].enabled = false;
            }
        }

        public void Init(CharacterBody owner)
        {
            Owner = owner;
            BaseDamage = owner.stats.Damage;

            for (int i = 0; i < dormantComponents.Length; i++)
            {
                IProjectileDormant initializedComponent = dormantComponents[i] as IProjectileDormant;
                if (initializedComponent != null)
                {
                    initializedComponent.Init(this);
                }
                dormantComponents[i].enabled = true;
            }
        }
    }
}