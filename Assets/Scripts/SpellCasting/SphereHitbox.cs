using UnityEngine;

namespace SpellCasting
{
    public class SphereHitbox : Hitbox
    {
        public override Collider[] DoOverlap()
        {
            return Physics.OverlapSphere(transform.position, transform.lossyScale.x * 0.5f);
        }
    }
}