using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
namespace SpellCasting
{
    [System.Serializable]
    public class CharacterStats
    {
        public float BaseMaxHealth = 100;
        public float BaseHealthRegen = 1;
        public float BaseDamage = 10;
        public float BaseAttackSpeed = 1;
        public float BaseStunFactor = 1;
        public float BaseKnockbackFactor = 1;

        public float BaseMaxMana = 100;
        public float BaseManaRegeneration = 1;
        public float BaseManaCostMultiplier = 1;

        public float BaseCastRange = 20;

        public float BaseMoveSpeed = 20;
        public float BaseJumpHeight;

        public VariableNumberStat MaxHealth;
        public VariableNumberStat HealthRegen;
        public VariableNumberStat Damage;
        public VariableNumberStat AttackSpeed;
        public VariableNumberStat StunFactor;
        public VariableNumberStat KnockbackFactor;

        public VariableNumberStat MaxFireMana;
        public VariableNumberStat MaxEarthMana;
        public VariableNumberStat MaxWaterMana;
        public VariableNumberStat MaxAirMana;
        public VariableNumberStat ManaRegeneration;
        public VariableNumberStat ManaCostMultiplier;

        public VariableNumberStat CastRange;

        public VariableNumberStat MoveSpeed;
        public VariableNumberStat JumpHeight;

        public void Init()
        {
            MaxHealth = new VariableNumberStat(BaseMaxHealth);
            HealthRegen = new VariableNumberStat(BaseHealthRegen);

            Damage = new VariableNumberStat(BaseDamage);
            AttackSpeed = new VariableNumberStat(BaseAttackSpeed);
            StunFactor = new VariableNumberStat(BaseStunFactor);
            KnockbackFactor = new VariableNumberStat(BaseKnockbackFactor);

            MaxFireMana = new VariableNumberStat(BaseMaxMana);
            MaxEarthMana = new VariableNumberStat(BaseMaxMana);
            MaxWaterMana = new VariableNumberStat(BaseMaxMana);
            MaxAirMana = new VariableNumberStat(BaseMaxMana);
            ManaRegeneration = new VariableNumberStat(BaseManaRegeneration);
            ManaCostMultiplier = new VariableNumberStat(BaseManaCostMultiplier);

            CastRange = new VariableNumberStat(BaseCastRange);

            MoveSpeed = new VariableNumberStat(BaseMoveSpeed);
            JumpHeight = new VariableNumberStat(BaseJumpHeight);
        }
    }

    [RequireComponent(typeof(CommonComponentsHolder))]
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(StateMachineLocator))]
    [RequireComponent(typeof(TeamComponent))]
    public class CharacterBody : MonoBehaviour, IHasCommonComponents
    {
        List<BuffInfo> activebuffs = new List<BuffInfo>();

        List<PowerItem> Inventory = new List<PowerItem>();

        public CharacterStats stats;

        [SerializeField]
        private CommonComponentsHolder commonComponents;
        public CommonComponentsHolder CommonComponents => commonComponents;

        [SerializeField]
        private bool dontDestroyOnLoad;

        public TeamIndex teamIndex => commonComponents.TeamComponent.TeamIndex;

        public bool Ded => commonComponents.HealthComponent.Ded;

        [ContextMenu("ReInitStats")]
        void Awake()
        {
            stats.Init();
            commonComponents.HealthComponent.Init(stats.MaxHealth);
            if (dontDestroyOnLoad)
            {
                Object.DontDestroyOnLoad(this);
            }
        }

        public void GiveItem(PowerItem powerItem)
        {
            AddBuff(powerItem.buffToApply);
        }

        public void AddBuff(BuffInfo buff)
        {
            if (activebuffs.Contains(buff))
                return;

            buff.OnApply(this);
            activebuffs.Add(buff);
        }

        public void AddTimedBuff(BuffInfo buff, float time)
        {
            BuffManager.AddBuffTimer(this, buff, time);
            AddBuff(buff);
        }

        public bool HasBuff(BuffInfo buff)
        {
            return activebuffs.Contains(buff);
        }

        public void Removebuff(BuffInfo buff)
        {
            if (activebuffs.Contains(buff))
            {
                buff.OnUnapply(this);
                activebuffs.Remove(buff);
            }
        }
    }
}