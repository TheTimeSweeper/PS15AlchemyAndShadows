using SpellCasting.World;
using System.Collections.Generic;
using UnityEngine;

namespace SpellCasting
{
    [System.Serializable]
    public class SavedData
    {
        public List<ElementType> UnlockedElements = new List<ElementType>();
    }

    public class MainGame : MonoBehaviour
    {
        [SerializeField]
        private RoomCatalog TEMPRoomCatalog;

        [SerializeField]
        private GameObject menu;

        public SavedData SavedData { get; private set; }

        void Awake()
        {
            InitializeGame();
        }

        public void InitializeGame()
        {
            string savedDataString = PlayerPrefs.GetString("SpellCastingGame_SavedData", string.Empty);

            if (!string.IsNullOrEmpty(savedDataString))
            {
                SavedData = JsonUtility.FromJson<SavedData>(savedDataString);
            }
            else
            {
                SavedData = new SavedData();
            }

            RoomCatalog.instance.InitializeAvailableRooms(SavedData.UnlockedElements);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menu.SetActive(!menu.activeInHierarchy);
                Time.timeScale = menu.activeInHierarchy ? 0 : 1;
            }
        }
    }
}