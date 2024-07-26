using SpellCasting.UI;
using UnityEngine;

namespace SpellCasting
{
    [DefaultExecutionOrder(-1)]
    public class AimMouseManager : MonoBehaviour
    {
        private Vector3 _lastMousePosition;
        private static Vector3 _aimMousePosition;
        public static Vector3 AimMousePosition { get => _aimMousePosition; set => _aimMousePosition = value; }

        public static AimMouseManager Instance;

        private static bool _wasUnlocked = CursorUnlocker.ReadOnlyInstancesList.Count <= 0;

        public delegate void CursorLockUpdatedEvent(bool unlocked);
        public static event CursorLockUpdatedEvent OnCursorLockUpdated;

        private void Awake()
        {
            if (Instance != null)
            {
                //jam i should have a copypaste base singleton class by now huh
                Debug.LogError($"tried to have duplicate singleton {this.GetType()}", this);
                this.enabled = false;
            }
            Instance = this;
        }

        void Update()
        {
            UpdateCursor();
        }

        public static void UpdateCursor()
        {
            bool cursorUnlocked = CursorUnlocker.ReadOnlyInstancesList.Count > 0;

            if (cursorUnlocked == _wasUnlocked)
            {
                return;
            }

            OnCursorLockUpdated?.Invoke(cursorUnlocked);

            _wasUnlocked = cursorUnlocked;
        }
    }
}