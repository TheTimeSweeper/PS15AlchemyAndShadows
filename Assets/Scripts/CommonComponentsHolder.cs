using ActiveStates;
using System.Security.Cryptography;
using UnityEngine;

namespace SpellCasting
{
    public class CommonComponentsHolder : MonoBehaviour
    {
        //if a class is added here, make sure to add the shorthand to ActiveState.cs
        //if this is jank please let me know why c:
        public HealthComponent HealthComponent;
        public InputBank InputBank;
        public CharacterBody CharacterBody;
        public Caster Caster;
        public FixedMotorDriver FixedMotorDriver;
        public CharacterModel CharacterModel;
        public StateMachineLocator StateMachineLocator;
        public TeamComponent TeamComponent;

        private void Reset()
        {
            HealthComponent = GetComponent<HealthComponent>();
            InputBank = GetComponent<InputBank>();
            CharacterBody = GetComponent<CharacterBody>();
            Caster = GetComponent<Caster>();
            FixedMotorDriver = GetComponent<FixedMotorDriver>();
            CharacterModel = GetComponent<CharacterModel>();
            StateMachineLocator = GetComponent<StateMachineLocator>();
            TeamComponent = GetComponent<TeamComponent>();
        }
    }
}