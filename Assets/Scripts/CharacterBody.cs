using UnityEngine;
namespace SpellCasting
{
    [System.Serializable]
    public class CharacterStats
    {
        public float BaseMaxHealth;
        public float BaseDamage;

        public float BaseMaxMana;

        public float BaseCastRange;

        public float BaseMoveSpeed;
        public float BaseJumpHeight;

        public VariableNumberStat MaxHealth;
        public VariableNumberStat Damage;

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

            MaxFireMana = new VariableNumberStat(BaseMaxMana);
            MaxEarthMana = new VariableNumberStat(BaseMaxMana);
            MaxWaterMana = new VariableNumberStat(BaseMaxMana);
            MaxAirMana = new VariableNumberStat(BaseMaxMana);

            CastRange = new VariableNumberStat(BaseCastRange);

            MoveSpeed = new VariableNumberStat(BaseMoveSpeed);
            JumpHeight = new VariableNumberStat(BaseJumpHeight);
        }
    }

    public class CharacterBody : MonoBehaviour
    {
        public CharacterStats stats;

        [SerializeField]
        private CommonComponentsHolder commonComponents;

        [ContextMenu("ReInitStats")]
        void Awake()
        {
            stats.Init();
            commonComponents.HealthComponent.Init(stats.MaxHealth);
        }
    }
}