using UnityEngine;

namespace SpellCasting
{
    public class CommonComponentsHolder : MonoBehaviour
    {
        public HealthComponent HealthComponent;
        public InputBank InputBank;
        public CharacterBody CharacterBody;
        public Caster Caster;
        public FixedMotorDriver FixedMotorDriver;

        private void Reset()
        {
            HealthComponent = GetComponent<HealthComponent>();
            InputBank = GetComponent<InputBank>();
            CharacterBody = GetComponent<CharacterBody>();
            Caster = GetComponent<Caster>();
            FixedMotorDriver = GetComponent<FixedMotorDriver>();
        }
    }
}