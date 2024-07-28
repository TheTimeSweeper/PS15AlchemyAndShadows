using UnityEngine;
namespace SpellCasting.World
{
    [CreateAssetMenu(menuName = "SpellCasting/UniqueRoom/Unspecified", fileName = "UniqueRoom")]
    public class UniqueRoom : ScriptableObject
    {
        [SerializeField]
        public Roome room;

        [SerializeField]
        public int maximumInstancesAllowed = 1;

        [SerializeField]
        public int minimumInstancesRequired = 1;

        //jam generalize this to a room scriptablobject and make uniqueroom use this function to check instances n stuff n things
        public virtual bool CanSpawn() { return true; }

        private void OnValidate()
        {
            minimumInstancesRequired = Mathf.Min(minimumInstancesRequired, maximumInstancesAllowed);
            maximumInstancesAllowed = Mathf.Max(maximumInstancesAllowed, minimumInstancesRequired);
        }
    }
}