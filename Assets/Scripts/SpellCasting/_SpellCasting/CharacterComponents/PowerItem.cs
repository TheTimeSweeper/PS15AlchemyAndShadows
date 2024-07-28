using UnityEngine;

namespace SpellCasting
{
    [CreateAssetMenu(menuName = "SpellCasting/Item", fileName = "Item")]
    public class PowerItem : ScriptableObject
    {
        [SerializeField]
        private string displayName;

        [SerializeField]
        private string description;

        [SerializeField]
        private Sprite icon;

        [SerializeField]
        public BuffInfo buffToApply;
    }
}