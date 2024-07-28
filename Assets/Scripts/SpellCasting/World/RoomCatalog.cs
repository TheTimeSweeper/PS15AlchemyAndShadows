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
        public List<Roome> Rooms;
    }

    public class RoomCatalog : Singleton<RoomCatalog>
    {
        private List<Roome> _allAvailableRooms;
        public List<Roome> AllAvailableRooms => _allAvailableRooms;

        public List<UniqueRoom> AllUniqueRooms { get; private set; }

        public List<ElementRequriedRooms> elementRequriedRooms;

        [SerializeField]
        public Roome StartRoom;

        [SerializeField]
        private List<UniqueRoom> uniqueRooms;

        //jam initialized by maingame. a base class or interface that maingame can loop through might be nice
        public void InitWithMainGame()
        {
            _allAvailableRooms = new List<Roome>();

            List<ElementType> availableElements = MainGame.Instance.SavedData.UnlockedElements;
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