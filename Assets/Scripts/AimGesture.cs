using System;
using UnityEngine;

namespace SpellCasting
{

    public enum GesturePriority
    {
        LOW,
        MID,
        HIGH
    }

    //JAM I would prefer to abstract these to be able to add more gestures
    //well I'm doin it anyway
    public enum GestureTypeIndex
    {
        SWIPE,
        SWIRL,
        SHAKE
    }

    public abstract class AimGesture : ScriptableObject, IComparable<AimGesture>
    {
        public abstract bool QualifyGesture(InputBank bank);
        public virtual void ResetGesture() { }

        [SerializeField]
        private GesturePriority priority;
        public GesturePriority Priority => priority;

        [SerializeField]
        public GestureTypeIndex index;
        public GestureTypeIndex Index => index;

        public int CompareTo(AimGesture other)
        {
            return other.Priority - Priority;
        }
    }
}