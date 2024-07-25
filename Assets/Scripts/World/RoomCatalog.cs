using System.Collections.Generic;
using UnityEngine;
namespace SpellCasting.World
{
    [System.Serializable]
    public class ElementRequriedRooms
    {
        public ElementType RequiredElement;
        public List<Room> Rooms;
    }

    public class RoomCatalog: MonoBehaviour
    {
        private List<Room> _allAvailableRooms = new List<Room>();
        public List<Room> AllAvaialbleRooms => _allAvailableRooms;

        public List<ElementRequriedRooms> elementRequriedRooms;

        public static RoomCatalog instance;

        void Awake()
        {
            instance = this;
        }

        //jam initialized by maingame. a base class or interface that maingame can loop through might be nice
        public void InitializeAvailableRooms(List<ElementType> availableElements)
        {
            for (int i = 0; i < elementRequriedRooms.Count; i++)
            {
                ElementRequriedRooms elementRooms = elementRequriedRooms[i];

                if (elementRooms.RequiredElement != null && !availableElements.Contains(elementRooms.RequiredElement))
                    continue;

                _allAvailableRooms.AddRange(elementRooms.Rooms);
            }
        }
    }
}