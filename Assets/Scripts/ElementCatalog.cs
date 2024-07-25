using System.Collections.Generic;
using UnityEngine;

namespace SpellCasting
{
    public class ElementCatalog : MonoBehaviour
    {
        public static Dictionary<ElementTypeIndex, ElementType> ElementTypes = new Dictionary<ElementTypeIndex, ElementType>();
        public static Dictionary<string, ElementType> NameToElementMap = new Dictionary<string, ElementType>();

        [SerializeField]
        private List<ElementType> TEMPElementTypes = new List<ElementType>();

        public void Init()
        {
            for (int i = 0; i < TEMPElementTypes.Count; i++)
            {
                ElementTypes.Add(TEMPElementTypes[i].Index, TEMPElementTypes[i]);
                NameToElementMap.Add(TEMPElementTypes[i].name, TEMPElementTypes[i]);
            }
        }
    }
}