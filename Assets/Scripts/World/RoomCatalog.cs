using System;
using System.Collections.Generic;
using UnityEngine;
namespace SpellCasting.World
{
    //jam should be a scriptableobject?
    [System.Serializable]
    public class ElementRequriedRooms
    {
        public ElementType RequiredElement;
        public List<Room> Rooms;
    }

    public class RoomCatalog: MonoBehaviour
    {
        private List<Room> _allAvailableRooms;
        public List<Room> AllAvailableRooms => _allAvailableRooms;

        public List<UniqueRoom> AllUniqueRooms;

        public List<ElementRequriedRooms> elementRequriedRooms;

        [SerializeField]
        private List<UniqueRoom> uniqueRooms;

        public static RoomCatalog instance;

        void Awake()
        {
            instance = this;
        }

        //jam initialized by maingame. a base class or interface that maingame can loop through might be nice
        public void InitializeAvailableRooms()
        {
            _allAvailableRooms = new List<Room>();

            List<ElementType> availableElements = MainGame.instance.SavedData.UnlockedElements;
            for (int i = 0; i < elementRequriedRooms.Count; i++)
            {
                ElementRequriedRooms elementRooms = elementRequriedRooms[i];

                if (elementRooms.RequiredElement != null && !availableElements.Contains(elementRooms.RequiredElement))
                    continue;

                _allAvailableRooms.AddRange(elementRooms.Rooms);
            }

            AllUniqueRooms = new List<UniqueRoom>();
            for (int i = 0; i < uniqueRooms.Count; i++)
            {
                if (uniqueRooms[i].CanSpawn())
                {
                    AllUniqueRooms.Add(uniqueRooms[i]);
                }
            }
        }
    }
}