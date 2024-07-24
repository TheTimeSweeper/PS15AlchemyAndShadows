using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Direction
{
    NORTH =  1,
    SOUTH = -1,
    EAST = 2,
    WEST = -2
}
namespace SpellCasting.World
{
    public class Room : MonoBehaviour
    {
        [SerializeField]
        private List<Door> doors;
        public List<Door> Doors => doors;

        [SerializeField]
        private List<OverlapZone> overlapZones;
        public List<OverlapZone> OverlapZones => overlapZones;

        [SerializeField]
        private float roomCost;
        public float RoomCost => roomCost;

        private Collider[] _colliders;

        private void OnValidate()
        {
            for (int i = 0; i < doors.Count; i++)
            {
                doors[i].DoorIndex = i;
            }
        }
    }
}