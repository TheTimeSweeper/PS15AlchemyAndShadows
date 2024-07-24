﻿using UnityEngine;

namespace SpellCasting
{
    [CreateAssetMenu(menuName = "ElementType/Water", fileName = "ElementWater")]
    public class ElementTypeWater : ElementType
    {

        [Header("Water")]
        [SerializeField]
        private float swipeSpawnSideDistance = 2;
        public float SwipeSpawnSideDistance => swipeSpawnSideDistance;
    }
}