using UnityEngine;

namespace SpellCasting
{
    public class BoxHitbox : Hitbox
    {
        public override Collider[] DoOverlap()
        {
            return Physics.OverlapBox(transform.position,  transform.lossyScale * 0.5f, transform.rotation);
        }
    }
}