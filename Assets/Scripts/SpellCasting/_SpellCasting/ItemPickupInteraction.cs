using UnityEngine;

namespace SpellCasting
{
    public class ItemPickupInteraction : InteractionHandler
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
