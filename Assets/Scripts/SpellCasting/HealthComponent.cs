using JetBrains.Annotations;
using UnityEngine;
namespace SpellCasting
{
    public class HealthComponent : MonoBehaviour, IHasCommonComponents
    {
        public delegate void HealthChangedEvent(float healthDelta);
        public event HealthChangedEvent OnHealthChange;

        public delegate void ModifyDamageCallback(DamageInfo damage);
        public event ModifyDamageCallback OnModifyDamage;

        public delegate void ModifyHealCallback(HealInfo damage);
        public event ModifyHealCallback OnModifyHeal;

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

        public void TakeDamage(DamageInfo damage)
        {
            OnModifyDamage?.Invoke(damage);

            health -= damage.Value;
            OnHealthChange?.Invoke(damage.Value);
        }
        public void Heal(HealInfo heal)
        {
            OnModifyHeal?.Invoke(heal);

            health += heal.Value;
            OnHealthChange?.Invoke(heal.Value);
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