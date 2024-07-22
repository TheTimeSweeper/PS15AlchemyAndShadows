using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellCasting
{
    public class NopeJs : MonoBehaviour
    {
        [SerializeField]
        private Vector3 rotation;

        [SerializeField]
        private float multiplier;

        void Update()
        {
            transform.Rotate(rotation * multiplier * Time.deltaTime);
        }
    }
}