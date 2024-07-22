using System.Collections.Generic;
using UnityEngine;

namespace SpellCasting
{
    [DefaultExecutionOrder(-1)]
    public class GestureCatalog : MonoBehaviour
    {
        public static Dictionary<GestureTypeIndex, AimGesture> Gestures = new Dictionary<GestureTypeIndex, AimGesture>();

        [SerializeField]
        private List<AimGesture> aimGestures = new List<AimGesture>();
        public static List<AimGesture> AllGestures;

        void Awake()
        {
            AllGestures = aimGestures;
        }
    }
}