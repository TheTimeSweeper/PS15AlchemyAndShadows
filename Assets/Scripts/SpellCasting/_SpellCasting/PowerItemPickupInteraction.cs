using UnityEngine;

namespace SpellCasting
{
    public class PowerItemPickupInteraction : InteractionHandler
    {
        [SerializeField]
        private PowerItem item;

        public override void OnBodyDetected(CharacterBody body, bool interactInputPressed)
        {
            body.GiveItem(item);
            Destroy(gameObject);
        }
    }
}
