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

        [SerializeField]
        private DeathComponent deathComponent;

        public bool Ded => health < 0;

        public void Init(float health)
        {
            maxHealth = health;
            this.health = health;
        }

        public void TakeDamage(DamagingInfo damage)
        {
            CharacterBody body = null;

            if (commonComponents != null)
            {
                body = commonComponents.CharacterBody;

            }

            GetDamagedinfo info = new GetDamagedinfo
            {
                VictimHealth = this,
                VictimBody = body,
                DamagingInfo = damage
            };

            DamageTypeCatalog.PreModifyDamageAll(info);

            PreModifyDamage?.Invoke(info);

            health -= damage.DamageValue;

            DamageTypeCatalog.OnTakeDamageAll(info);

            OnDamageTaken?.Invoke(info);

            if (Ded && deathComponent != null)
            {
                deathComponent.GetRektLol();
            }
        }

        public void Heal(HealingInfo heal)
        {
            PreModifyHeal?.Invoke(heal);

            health = Mathf.Clamp(health + heal.HealValue, 0, maxHealth);
            OnHealTaken?.Invoke(heal.HealValue);
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

        void FixedUpdate()
        {
            if (commonComponents != null && commonComponents.CharacterBody != null)
            {
                float regenPerSecond = commonComponents.CharacterBody.stats.HealthRegenPercent * commonComponents.CharacterBody.stats.MaxHealth;

                health = Mathf.Clamp(health + regenPerSecond * Time.fixedDeltaTime, 0, maxHealth);
            }
        }
    }
}