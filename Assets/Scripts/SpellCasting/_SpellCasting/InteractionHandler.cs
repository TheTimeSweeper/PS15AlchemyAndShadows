using UnityEngine;

namespace SpellCasting
{
    public abstract class InteractionHandler : MonoBehaviour
    {
        public abstract void OnBodyDetected(CharacterBody body, bool interactInputPressed);
    }
}
