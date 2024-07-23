using UnityEngine;

namespace SpellCasting
{                          //woops misnamed should probably be HitBox but I am in no way changing that and replacing all the components on every prefab that has a hitbox
    public abstract class Hitbox : MonoBehaviour
    {
        public abstract Collider[] DoOverlap();
    }
}