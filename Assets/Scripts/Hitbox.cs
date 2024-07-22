using UnityEngine;

namespace SpellCasting
{
    public abstract class Hitbox : MonoBehaviour
    {
        public abstract Collider[] DoOverlap();
    }
}