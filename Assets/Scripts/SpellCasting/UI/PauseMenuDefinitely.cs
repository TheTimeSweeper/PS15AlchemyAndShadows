using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

namespace SpellCasting.UI
{
    public class PauseMenuDefinitely : MonoBehaviour
    {
        [SerializeField]
        private GameObject menuObject;

        [SerializeField]
        private Button titleButton;

        private void Awake()
        {
            titleButton.onClick.AddListener(titleClick);
            menuObject.SetActive(false);
        }

        private void Update()
        {
            if (MainGame.EscapeInput.JustPressed(this))
            {
                menuObject.SetActive(!menuObject.activeInHierarchy);
            }
        }

        private void titleClick()
        {
            ConfirmPopup.Open("Do you want to reset your run?", "yes", "no", () =>
            {
                menuObject.SetActive(false);
                LevelProgressionManager.TrueReset();
            });
        }
    }
}