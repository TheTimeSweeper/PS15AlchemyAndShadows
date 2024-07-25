using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
namespace SpellCasting.World
{
    public class Level: MonoBehaviour
    {
        [SerializeField]
        private List<Room> existingRooms;
        //public List<Room> Rooms => rooms;

        private List<Room> _existingRoomQueue;

        [SerializeField]
        private float TEMPStartingCredits = 120;

        [SerializeField]
        private float availableCredits;

        void Awake()
        {
            GenerateLevel();
        }

        Dictionary<string, int> roomLog = new Dictionary<string, int>();

        [ContextMenu("ReGenerate")]
        public void RegenerateLevel()
        {
            for (int i = 0; i < existingRooms.Count; i++)
            {
                Destroy(existingRooms[i].gameObject);
            }
            existingRooms = new List<Room>
            {
                Instantiate(RoomCatalog.instance.AllAvaialbleRooms[0], Vector3.zero, Quaternion.identity, transform)
            };

            Invoke("GenerateLevel", 0.2f);
        }

        public void GenerateLevel()
        {
            _existingRoomQueue = new List<Room>(existingRooms);

            availableCredits = TEMPStartingCredits;

            float lowestCost = float.MaxValue;
            for (int i = 0; i < RoomCatalog.instance.AllAvaialbleRooms.Count; i++)
            {
                float cost = RoomCatalog.instance.AllAvaialbleRooms[i].RoomCost;
                if (cost > 0)
                {
                    lowestCost = RoomCatalog.instance.AllAvaialbleRooms[i].RoomCost;
                }
            }

            int failsafe = 0;
            while (_existingRoomQueue.Count > 0 && availableCredits > lowestCost)
            {
                failsafe++;
                if(failsafe > 10000)
                {
                    Debug.LogError($"FAILSAFE: credits {availableCredits}, roomqueue {_existingRoomQueue.Count}");
                    break;
                }

                _existingRoomQueue.Shuffle();

                Room existingRoom = _existingRoomQueue[0];
                existingRoom.Doors.Shuffle();
                for (int j = 0; j < existingRoom.Doors.Count; j++)
                {
                    Door existingDoor = existingRoom.Doors[j];

                    int occupiedDoors = 0;
                    if (existingDoor.Occupied)
                    {
                        occupiedDoors++;
                        //all doors occupied, remove this room
                        if (j == existingRoom.Doors.Count - 1 && occupiedDoors >= existingRoom.Doors.Count - 1)
                        {
                            _existingRoomQueue.Remove(existingRoom);
                        }
                        break;
                    }

                    List<Room> potentialRooms = new List<Room>(RoomCatalog.instance.AllAvaialbleRooms);

                    int tries = 0;
                    while (potentialRooms.Count > 0)
                    {
                        tries++;
                        if (tries > 10000)
                        {
                            Debug.LogError($"FAILSAFE: credits {availableCredits}, roomqueue {_existingRoomQueue.Count}, potentialRooms{potentialRooms.Count}");
                            break;
                        }

                        Room potentialRoom = potentialRooms.WeightedRandom((room) => room.RoomWeight);

                        if (potentialRoom == null)
                        {
                            break;
                        }

                        if (potentialRoom.RoomCost > availableCredits)
                        {
                            potentialRooms.Remove(potentialRoom);
                            continue;
                        }

                        bool spawned = false;

                        List<Door> potentialRoomDoors = new List<Door>(potentialRoom.Doors);
                        potentialRoomDoors.Shuffle();
                        for (int l = 0; l < potentialRoomDoors.Count; l++)
                        {
                            Door potentialDoor = potentialRoomDoors[l];
                            if ((int)potentialDoor.FacingDirection != -(int)existingDoor.FacingDirection)
                                continue;

                            bool overlapped = false;

                            for (int m = 0; m < potentialRoom.OverlapZones.Count; m++)
                            {
                                OverlapZone overlapZone = potentialRoom.OverlapZones[m];
                                Vector3 center = overlapZone.localPosition - potentialDoor.transform.localPosition + existingDoor.transform.position;

                                Collider[] overlaps = Physics.OverlapBox(center, overlapZone.localScale * 0.5f, Quaternion.identity, LayerInfo.RoomOverlap.layerMask);
                                overlapped |= overlaps.Length > 0;

                                //Debug.LogWarning($"p room {potentialRoom.name}, checking p overlapzone {overlapZone} with respect to p door {potentialDoor.name}," +
                                //    $"checked against e door {existingDoor.name} and found {overlaps.Length} overlaps");
                                //TestCube(overlaps.Length > 0, center, overlapZone.localScale, $"p{potentialRoom.name} p{overlapZone.name} p {potentialDoor.name} e {existingDoor.name}");
                            }

                            if (!overlapped)
                            {
                                spawned = true;
                                Vector3 position = potentialRoom.transform.position - potentialDoor.transform.position + existingDoor.transform.position;

                                Room newRoom = Instantiate(potentialRoom, position, Quaternion.identity, transform);
                                existingRooms.Add(newRoom);
                                _existingRoomQueue.Add(newRoom);

                                newRoom.Doors[potentialDoor.DoorIndex].Occupied = true;
                                newRoom.Doors[potentialDoor.DoorIndex].GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.red);

                                existingDoor.Occupied = true;
                                existingDoor.GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.magenta);
                                availableCredits -= potentialRoom.RoomCost;
                            }
                        }
                        if (!spawned)
                        {
                            potentialRooms.Remove(potentialRoom);
                            continue;
                        }

                        if (potentialRooms.Count == 0)
                        {
                            existingDoor.Occupied = true;
                            existingDoor.GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.yellow);
                        }
                    }

                    //for (int i = 0; i < existingRooms.Count; i++)
                    //{
                    //    var ROom = existingRooms[i];
                    //    for (int x = 0; x < ROom.Doors.Count; x++)
                    //    {
                    //        if (ROom.Doors[x].Occupied)
                    //        {
                    //            ROom.Doors[x].GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.blue);
                    //        }
                    //    }
                    //}
                }
            }

            for (int i = 0; i < existingRooms.Count; i++)
            {
                if (!roomLog.ContainsKey(existingRooms[i].name))
                {
                    roomLog[existingRooms[i].name] = 0;
                }
                roomLog[existingRooms[i].name]++;
            }

            string readout = "room distribution:";
            foreach (var lo in roomLog)
            {
                readout += $"\n{lo.Key}: {lo.Value}";
            }
            Debug.Log(readout);
        }

        // I've passed the 200 line script line noooooo
        public void TestCube(bool overlapped, Vector3 position, Vector3 scale, string name)
        {
            var cob = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cob.name = name;
            cob.transform.position = position;
            cob.transform.localScale = scale;

            if (overlapped)
            {
                cob.GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.magenta);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                RegenerateLevel();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                roomLog = new Dictionary<string, int>();
            }
        }
    }
}