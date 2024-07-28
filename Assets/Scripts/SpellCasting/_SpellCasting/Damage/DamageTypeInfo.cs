﻿using ActiveStates;
using UnityEngine;
namespace SpellCasting
{
    [CreateAssetMenu(menuName = "SpellCasting/DamageType/Generic", fileName = "DamageType")]
    public class DamageTypeInfo : ScriptableObject
    {
        [SerializeField]
        public DamageTypeIndex damageTypeIndex;

        public virtual void PreModifyDamage(GetDamagedinfo damagedInfo) { }
        public virtual void OnTakeDamage(GetDamagedinfo damagedInfo) { }
    }
}