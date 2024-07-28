using UnityEngine;

namespace SpellCasting
{
    public abstract class InteractionHandler : MonoBehaviour
    {
        public abstract void HandleInteraction(CharacterBody body);
    }
}
