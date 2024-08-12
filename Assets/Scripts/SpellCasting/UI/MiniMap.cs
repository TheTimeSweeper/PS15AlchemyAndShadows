using UnityEngine;

namespace SpellCasting.UI
{
    public class MiniMap : MonoBehaviour
    {
        [SerializeField]
        private Transform cameraPivot;

        private CharacterBody _centerPlayer;

        private void Update()
        {
            if (_centerPlayer == null)
            {
                _centerPlayer = CharacterBodyTracker.FindPrimaryPlayer();
            } else
            {
                cameraPivot.transform.position = _centerPlayer.transform.position;
            }
        }
    }
}