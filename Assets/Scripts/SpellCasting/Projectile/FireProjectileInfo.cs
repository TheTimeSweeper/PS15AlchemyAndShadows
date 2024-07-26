using UnityEngine;

namespace SpellCasting.Projectiles
{
    public class FireProjectileInfo
    {
        public GameObject OwnerObject;
        public CharacterBody OwnerBody;
        public Vector3 AimDirection;
        public Vector3 StartPosition;
        public Vector3 PreviousPosition;
        public TeamIndex TeamIndex;
        public float Damage = 1;
    }
}