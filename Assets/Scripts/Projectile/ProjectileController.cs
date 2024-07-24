using System.Collections.Generic;
using UnityEngine;

namespace SpellCasting.Projectiles
{
    [RequireComponent(typeof(TeamComponent))]
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField]
        private List<MonoBehaviour> subComponentsBehaviours;

        [SerializeField]
        private TeamComponent teamComponent;

        [SerializeField]
        private MonoBehaviour[] dormantComponents;

        public float BaseDamage { get; set; }
        //jam todo teamcomponent maybe
        public CharacterBody Owner { get; set; }
        public TeamIndex TeamIndex => teamComponent.TeamIndex;

        void Reset()
        {
            AssignSubComponents();
            teamComponent = GetComponent<TeamComponent>();
        }

        [ContextMenu("Assign SubComponents")]
        private void AssignSubComponents()
        {
            subComponentsBehaviours.Clear();
            IProjectileSubComponent[] attachedSubComponents = GetComponents<IProjectileSubComponent>();
            for (int i = 0; i < attachedSubComponents.Length; ++i)
            {
                subComponentsBehaviours.Add((MonoBehaviour)attachedSubComponents[i]);
            }
        }

        void Awake()
        {
            for (int i = 0; i < subComponentsBehaviours.Count; i++)
            {
                Object subComponent = subComponentsBehaviours[i];
                ((IProjectileSubComponent)subComponent).Controller = this;
            }

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
                    initializedComponent.Init();
                }
                dormantComponents[i].enabled = true;
            }
        }
    }
}