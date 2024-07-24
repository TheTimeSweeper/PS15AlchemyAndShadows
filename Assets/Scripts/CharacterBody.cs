using UnityEngine;
namespace SpellCasting
{
    [System.Serializable]
    public class CharacterStats
    {
        public float BaseMaxHealth = 100;
        public float BaseDamage = 10;
        public float BaseAttackSpeed = 1;

        public float BaseMaxMana = 100;

        public float BaseCastRange = 20;

        public float BaseMoveSpeed = 20;
        public float BaseJumpHeight;

        public VariableNumberStat MaxHealth;
        public VariableNumberStat Damage;
        public VariableNumberStat AttackSpeed;

        public VariableNumberStat MaxFireMana;
        public VariableNumberStat MaxEarthMana;
        public VariableNumberStat MaxWaterMana;
        public VariableNumberStat MaxAirMana;

        public VariableNumberStat CastRange;

        public VariableNumberStat MoveSpeed;
        public VariableNumberStat JumpHeight;

        public void Init()
        {
            MaxHealth = new VariableNumberStat(BaseMaxHealth);
            Damage = new VariableNumberStat(BaseDamage);
            AttackSpeed = new VariableNumberStat(BaseAttackSpeed);

            MaxFireMana = new VariableNumberStat(BaseMaxMana);
            MaxEarthMana = new VariableNumberStat(BaseMaxMana);
            MaxWaterMana = new VariableNumberStat(BaseMaxMana);
            MaxAirMana = new VariableNumberStat(BaseMaxMana);

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

        [ContextMenu("ReInitStats")]
        void Awake()
        {
            stats.Init();
            commonComponents.HealthComponent.Init(stats.MaxHealth);
        }
    }
}