using UnityEngine.SceneManagement;
namespace SpellCasting
{
    public class LevelProgressionManager : Singleton<LevelProgressionManager>
    {
        public static int DifficultyProgression;

        protected override void InitOnce()
        {
            base.InitOnce();
        }

        public override void ReInit()
        {
            base.ReInit();
            DifficultyProgression = 0;
        }

        public static void TrueReset()
        {
            SceneManager.MoveGameObjectToScene(Instance.gameObject, SceneManager.GetActiveScene());
            Instance = null;

            var player = CharacterBodyTracker.FindPrimaryPlayer();
            if (player != null)
            {
                SceneManager.MoveGameObjectToScene(player.gameObject, SceneManager.GetActiveScene());

            }
            SceneManager.LoadScene(0);
        }
    }
}