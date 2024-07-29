using UnityEngine;

namespace SpellCasting
{
    public class ElementUnlockInteraction : InteractionHandler
    {
        [SerializeField]
        private ElementType elementToUnlock;

        [SerializeField]
        private GameObject fancyEffects;

        [SerializeField]
        private GameObject elementDisplay;

        void Awake()
        {
            Instantiate(elementToUnlock.ElementMassPrefab, elementDisplay.transform.position, elementDisplay.transform.rotation, elementDisplay.transform);
        }

        public override void OnBodyDetected(CharacterBody body, bool InteractPressed)
        {
            if (!InteractPressed)
                return;

            MainGame.Instance.SavedData.AddElement(elementToUnlock);
            MainGame.Instance.SavedData.Save();

            if (fancyEffects != null)
            {
                fancyEffects.SetActive(true);
            }

            if (elementDisplay != null)
            {
                elementDisplay.SetActive(false);
            }
        }
    }
}
