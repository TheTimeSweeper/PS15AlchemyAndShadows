using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace SpellCasting.World
{
    public class Level: MonoBehaviour
    {
        [SerializeField]
        private List<Room> existingRooms;
        //public List<Room> Rooms => rooms;

        private Queue<Room> _existingRoomQueue;

        private List<Room> _randomAvailableRooms;


        [SerializeField]
        private float TEMPCredits = 1000;

        void Awake()
        {
            GenerateLevel();    
        }

        [ContextMenu("ReGenerate")]
        public void RegenerateLevel()
        {
            for (int i = 0; i < existingRooms.Count; i++)
            {
                Destroy(existingRooms[i].gameObject);
            }
            existingRooms = new List<Room>
            {
                Instantiate(RoomCatalog.instance.RoomPrefabs[Random.Range(0, RoomCatalog.instance.RoomPrefabs.Count)], Vector3.zero, Quaternion.identity, transform)
            };

            Invoke("GenerateLevel", 0.2f);
        }

        public void GenerateLevel()
        {
            _existingRoomQueue = new Queue<Room>(existingRooms);

            float credits = TEMPCredits;

            while (_existingRoomQueue.Count > 0 && credits > 0)
            {
                Room existingRoom = _existingRoomQueue.Dequeue();
                for (int j = 0; j < existingRoom.Doors.Count; j++)
                {
                    Door existingDoor = existingRoom.Doors[j];
                    if (existingDoor.Occupied)
                        continue;

                    _randomAvailableRooms = new List<Room>(RoomCatalog.instance.RoomPrefabs);
                    _randomAvailableRooms.Sort((room1, room2) => { return Random.value > 0.5f ? -1 : 1; });

                    for (int k = 0; k < _randomAvailableRooms.Count; k++)
                    {
                        Room potentialRoom = _randomAvailableRooms[k];
                        if (potentialRoom.RoomCost > credits)
                            continue;

                        for (int l = 0; l < potentialRoom.Doors.Count; l++)
                        {
                            Door potentialDoor = potentialRoom.Doors[l];
                            if ((int)potentialDoor.FacingDirection != -(int)existingDoor.FacingDirection)
                                continue;

                            bool overlapped = false;
                            for (int m = 0; m < potentialRoom.OverlapZones.Count; m++)
                            {
                                OverlapZone overlapZone = potentialRoom.OverlapZones[m];
                                Vector3 center = overlapZone.localPosition - potentialDoor.transform.localPosition + existingDoor.transform.position;

                                Collider[] overlaps = Physics.OverlapBox(center, overlapZone.localScale * 0.5f, Quaternion.identity, LayerInfo.RoomOverlap.layerMask);
                                Debug.LogWarning($"p room {potentialRoom.name}, checking p overlapzone {overlapZone} with respect to p door {potentialDoor.name}," +
                                    $"checked against e door {existingDoor.name} and found {overlaps.Length} overlaps");
                                //TestCube(center, overlapZone.localScale, $"p{potentialRoom.name} p{overlapZone.name} p {potentialDoor.name} e {existingDoor.name}");
                                overlapped |= overlaps.Length > 0;
                            }

                            if (!overlapped)
                            {
                                Vector3 position = potentialRoom.transform.position - potentialDoor.transform.position + existingDoor.transform.position;

                                Room newRoom = Instantiate(potentialRoom, position, Quaternion.identity, transform);
                                existingRooms.Add(newRoom);
                                _existingRoomQueue.Enqueue(newRoom);

                                newRoom.Doors[potentialDoor.DoorIndex].Occupied = true;
                                existingDoor.Occupied = true;
                                credits -= potentialRoom.RoomCost;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < existingRooms.Count; i++)
            {
                var room = existingRooms[i];
                for (int j = 0; j < room.Doors.Count; j++)
                {
                    if (!room.Doors[j].Occupied)
                    {
                        room.Doors[j].gameObject.SetActive(false);
                    }
                }
            }
        }

        public void TestCube(Vector3 position, Vector3 scale, string name)
        {
            var cob = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cob.name = name;
            cob.transform.position = position;
            cob.transform.localScale = scale;
        }
    }
}