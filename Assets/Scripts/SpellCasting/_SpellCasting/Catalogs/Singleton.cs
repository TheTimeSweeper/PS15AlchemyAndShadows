using UnityEngine;

namespace SpellCasting
{
    public abstract class Singleton<T>: MonoBehaviour where T : Singleton<T>
    {
        public static T Instance;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                Instance.ReInit();
                return;
            }
            Instance = this as T;
            InitOnce();
        }

        protected virtual void InitOnce() { }
        public virtual void ReInit() { }
    }
}