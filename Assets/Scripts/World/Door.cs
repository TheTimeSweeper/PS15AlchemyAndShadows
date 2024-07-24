using UnityEngine;
namespace SpellCasting.World
{
    public class Door : MonoBehaviour
    {
        [SerializeField]
        private Transform closedObject;

        [SerializeField]
        private Direction facingDirection = Direction.NORTH;
        public Direction FacingDirection => facingDirection;

        [SerializeField]
        private bool occupied;
        public bool Occupied { get => occupied; set => occupied = value; }

        [SerializeField]
        private int doorIndex;
        public int DoorIndex { get => doorIndex; set => doorIndex = value; }
    }
}