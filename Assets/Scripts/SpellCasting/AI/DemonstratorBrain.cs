using UnityEngine;

namespace SpellCasting.AI
{
    public class DemonstratorBrain : AIBrain
    {
        [SerializeField]
        private CharacterBody targetBody;

        public override CharacterBody SearchForTarget()
        {
            return targetBody;
        }
    }
}
