using SpellCasting.Projectiles;
using System;
using UnityEngine;

namespace SpellCasting
{
    public class ElementSubMass : MonoBehaviour
    {
        [SerializeField]
        private Transform viewContainer;

        [SerializeField]
        private Rigidbody rigidBody;

        [SerializeField]
        private ProjectileController projectileController;

        public void MoveTowards(Vector3 position)
        {
            transform.position = Vector3.Lerp(transform.position, position, 0.1f);
        }

        public void SetMass(float totalMass)
        {
            viewContainer.transform.localScale = Vector3.one * totalMass;
        }

        public void JAMThrow(Vector3 throwVector)
        {
            rigidBody.isKinematic = false;
            rigidBody.velocity = throwVector;
        }

        public void JAMActivateProjectile(CharacterBody owner)
        {
            projectileController.Init(owner);
        }
    }
}
