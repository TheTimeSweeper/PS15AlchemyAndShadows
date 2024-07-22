using JetBrains.Annotations;
using UnityEngine;
namespace SpellCasting
{
    public class HealthComponent : MonoBehaviour
    {
        public delegate void HealthChangedEvent(float healthDelta);
        public event HealthChangedEvent OnHealthChange;

        [SerializeField]
        private float health;
        public float Health { get => health; }

        [SerializeField]
        private float maxHealth;
        public float MaxHealth { get => maxHealth; }

        [SerializeField]
        private CommonComponentsHolder commonComponents;
        public CommonComponentsHolder CommonComponents => commonComponents;

        public void Init(float health)
        {
            maxHealth = health;
            this.health = health;
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            OnHealthChange?.Invoke(damage);
        }
        public void Heal(float heal)
        {
            health += heal;
            OnHealthChange?.Invoke(heal);
        }

        public void UpdateMaxHealth(float newMaxHealth, bool heal)
        {
            float maxHealthDelta = newMaxHealth - maxHealth;
            maxHealth = newMaxHealth;
            if (heal)
            {
                health += maxHealthDelta;
            }
        }
    }
}