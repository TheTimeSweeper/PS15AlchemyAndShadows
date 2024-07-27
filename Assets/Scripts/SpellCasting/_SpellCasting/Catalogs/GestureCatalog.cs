using System.Collections.Generic;
using UnityEngine;

namespace SpellCasting
{
    public class GestureCatalog : MonoBehaviour
    {
        [SerializeField]
        private List<AimGesture> aimGestures = new List<AimGesture>();
        public static List<AimGesture> AllGestures;

        //jam not a singleton. replaces gestures whenever a new one is loaded, that's fine
        void Awake()
        {
            AllGestures = aimGestures;
            aimGestures.Sort();
        }
    }
}