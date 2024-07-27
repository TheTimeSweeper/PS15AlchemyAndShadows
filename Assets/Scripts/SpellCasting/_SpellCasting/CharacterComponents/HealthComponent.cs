using JetBrains.Annotations;
using UnityEngine;
namespace SpellCasting
{
    public class HealthComponent : MonoBehaviour, IHasCommonComponents
    {
        public delegate void DamageTakenEvent(GetDamagedinfo getDamagedInfo);
        public event DamageTakenEvent OnDamageTaken;
        public delegate void ModifyDamageCallback(GetDamagedinfo getDamagedInfo);
        public event ModifyDamageCallback PreModifyDamage;

        public delegate void HealTakenEvenet(float healAmount);
        public event HealTakenEvenet OnHealTaken;
        public delegate void ModifyHealCallback(HealingInfo damage);
        public event ModifyHealCallback PreModifyHeal;

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

        public void TakeDamage(DamagingInfo damage)
        {
            GetDamagedinfo info = new GetDamagedinfo
            {
                VictimHealth = this,
                VictimBody = commonComponents.CharacterBody,
                DamagingInfo = damage
            };

            DamageTypeCatalog.PreModifyDamageAll(info);

            PreModifyDamage?.Invoke(info);

            health -= damage.DamageValue;

            DamageTypeCatalog.OnTakeDamageAll(info);

            OnDamageTaken?.Invoke(info);
        }

        public void Heal(HealingInfo heal)
        {
            PreModifyHeal?.Invoke(heal);

            health += heal.DamageValue;
            OnHealTaken?.Invoke(heal.DamageValue);
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