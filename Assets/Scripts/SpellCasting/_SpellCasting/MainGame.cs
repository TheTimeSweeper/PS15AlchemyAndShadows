using SpellCasting.World;
using UnityEngine;
using UnityEngine.UIElements;

namespace SpellCasting
{
    public class MainGame : MonoBehaviour
    {
        [SerializeField]
        private RoomCatalog TEMPRoomCatalog;

        [SerializeField]
        private ElementCatalog TEMPElementCatalog;

        [SerializeField]
        private GameObject menu;

        public static MainGame instance { get; private set; }

        private InputState escapeInput = new InputState();

        public SavedData SavedData { get; private set; }

        void Awake()
        {
            instance = this;
            InitializeGame();
        }

        public void InitializeGame()
        {
            TEMPElementCatalog.Init();

            SavedData = SavedData.LoadOrCreate();

            RoomCatalog.instance.InitializeAvailableRooms();
        }

        //jam ... yeah
        private void Update()
        {
            escapeInput.UpdateInput(Input.GetKey(KeyCode.Escape));

            if (escapeInput.JustPressed(this))
            {
                menu.SetActive(!menu.activeInHierarchy);
                Time.timeScale = menu.activeInHierarchy ? 0 : 1;
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                SavedData.AddElement("ElementFire");
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                SavedData.RemoveElement("ElementFire");
            }
        }
    }
}