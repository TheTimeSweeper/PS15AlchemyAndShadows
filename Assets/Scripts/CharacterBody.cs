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
        public float BaseStunResistance = 0;

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
        public VariableNumberStat StunResistance;

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
            StunResistance = new VariableNumberStat(BaseStunResistance);

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

    public class CharacterBody : MonoBehaviour, IHasCommonComponents
    {
        public CharacterStats stats;

        [SerializeField]
        private CommonComponentsHolder commonComponents;
        public CommonComponentsHolder CommonComponents => commonComponents;

        public TeamIndex teamIndex => commonComponents.TeamComponent.TeamIndex;

        [ContextMenu("ReInitStats")]
        void Awake()
        {
            stats.Init();
            commonComponents.HealthComponent.Init(stats.MaxHealth);
        }
    }
}