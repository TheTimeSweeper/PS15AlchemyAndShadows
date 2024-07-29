using UnityEngine;
namespace SpellCasting
{
    public class EnemyDifficultyModifier : MonoBehaviour {

        [SerializeField]
        private CharacterBody charBod;

        void Start()
        {
            charBod.stats.MaxHealth.ApplyAddModifier(50 * LevelProgressionManager.DifficultyProgression, "difficulty");
        }
    }
}