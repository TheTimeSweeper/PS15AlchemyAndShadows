using UnityEngine;
namespace SpellCasting.World
{
    [CreateAssetMenu(menuName = "UniqueRoom/RoomThatDoesntShowUpIfYouUnlockedElements", fileName = "UniqueRoom")]
    public class RoomThatDoesntShowUpIfYouUnlockedElements : UniqueRoom
    {
        [SerializeField]
        private ElementType elementThatTheRoomDoesntShowUpIfYouUnlock;

        public override bool CanSpawn()
        {
            return !MainGame.Instance.SavedData.UnlockedElements.Contains(elementThatTheRoomDoesntShowUpIfYouUnlock);
        }
    }
}