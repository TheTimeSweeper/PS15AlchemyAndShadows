﻿using SpellCasting.Projectiles;
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpellCasting
{
    public class ElementSubMass : MonoBehaviour
    {
        [SerializeField, FormerlySerializedAs("viewContainer")]
        private Transform massScaleContainer;

        [SerializeField]
        private Rigidbody rigidBody;

        [SerializeField]
        private ProjectileController projectileController;

        public float CurrentMass { get; set; }

        public void SetMass(float subMass)
        {
            massScaleContainer.transform.localScale = Vector3.one * subMass;
            CurrentMass = subMass;
        }

        public void JAMActivateProjectile(CharacterBody owner, Vector3 throwVector, Vector3 throwPosition, Vector3 originalPosition, DamageTypeIndex damageType)
        {
            projectileController?.Init(new FireProjectileInfo
            {
                OwnerObject = owner.gameObject,
                OwnerBody = owner,
                Damage = owner.stats.Damage,
                DamageType = damageType,
                PreviousPosition = originalPosition,
                StartPosition = throwPosition,
                AimDirection = throwVector,
                TeamIndex = owner.teamIndex
            });
        }
    }
}
