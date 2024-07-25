using System;
using UnityEngine;
namespace SpellCasting.World
{
    [ExecuteAlways]
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

        [SerializeField]
        private GameObject obstruction;

        public void SetClosed(bool closed)
        {
            if(obstruction != null)
                obstruction.SetActive(closed);

            GetComponentInChildren<Renderer>()?.material.SetColor("_Color", closed ? Color.red : Color.cyan);
        }

        public void OnValidate()
        {
            var nip = GetComponentInParent<Room>();
            if (nip == null)
                return;

            nip.OnValidate();
        }

        private void OnDestroy()
        {
            var nip = GetComponentInParent<Room>();
            if (nip == null)
                return;

            Debug.LogError($"don't forget to remove this from the Room {nip.name}", nip);
        }
    }
}