using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace SpellCasting
{
    public class LevelProgressionManager : Singleton<LevelProgressionManager>
    {
        public static int DifficultyProgression;

        public static bool resetting;

        protected override void InitOnce()
        {
            base.InitOnce();
            resetting = false;
        }

        public override void ReInit()
        {
            base.ReInit();
            DifficultyProgression++;
            resetting = false;
        }

        public IEnumerator DelayReset()
        {
            yield return new WaitForSeconds(1);
            TrueReset();
        }

        public static void TrueReset()
        {
            DifficultyProgression = 0;
            resetting = true;
            SceneManager.MoveGameObjectToScene(Instance.gameObject, SceneManager.GetActiveScene());
            Instance = null;

            var player = CharacterBodyTracker.FindPrimaryPlayer();
            if (player != null)
            {
                SceneManager.MoveGameObjectToScene(player.gameObject, SceneManager.GetActiveScene());

            }

            Destroy(Camera.main.gameObject);
            SceneManager.LoadScene(0);
        }

        public static void DelayedTrueReset()
        {
            if (resetting)
                return;

            resetting = true;
            Instance.StartCoroutine(Instance.DelayReset());
        }
    }
}