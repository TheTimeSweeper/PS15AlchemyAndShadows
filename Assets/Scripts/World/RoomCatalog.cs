using System.Collections.Generic;
using UnityEngine;
namespace SpellCasting.World
{
    public class RoomCatalog: MonoBehaviour
    {
        [SerializeField]
        private List<Room> roomPrefabs;
        public List<Room> RoomPrefabs { get => roomPrefabs; }

        public static RoomCatalog instance;

        void Awake()
        {
            instance = this;
        }
    }
}