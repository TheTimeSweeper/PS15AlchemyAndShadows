using System.Collections.Generic;
using UnityEngine;

namespace SpellCasting
{
    public class ElementCatalog : MonoBehaviour
    {
        public static Dictionary<ElementTypeIndex, ElementType> ElementTypes = new Dictionary<ElementTypeIndex, ElementType>();

        [SerializeField]
        private List<ElementType> TEMPElementTypes = new List<ElementType>();

        private void Awake()
        {
            for (int i = 0; i < TEMPElementTypes.Count; i++)
            {
                ElementTypes.Add(TEMPElementTypes[i].Index, TEMPElementTypes[i]);
            }
        }
    }
}