using UnityEngine;

namespace SpellCasting
{
    public class MainGame : MonoBehaviour
    {
        [SerializeField]
        private GameObject menu;

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