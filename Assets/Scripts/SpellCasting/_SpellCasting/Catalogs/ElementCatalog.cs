using System.Collections.Generic;
using UnityEngine;

namespace SpellCasting
{
    public class ElementCatalog : Singleton<ElementCatalog>
    {
        public Dictionary<ElementTypeIndex, ElementType> ElementTypes = new Dictionary<ElementTypeIndex, ElementType>();
        public Dictionary<string, ElementType> NameToElementMap = new Dictionary<string, ElementType>();

        [SerializeField]
        private List<ElementType> TEMPElementTypes = new List<ElementType>();

        public static ElementType TryCombineElements(ElementType element1, ElementType element2)
        {
            //todo combine elements
            return null;
        }

        public void InitWithMainGame()
        {
            for (int i = 0; i < TEMPElementTypes.Count; i++)
            {
                ElementTypes.Add(TEMPElementTypes[i].Index, TEMPElementTypes[i]);
                NameToElementMap.Add(TEMPElementTypes[i].name, TEMPElementTypes[i]);
            }
        }
    }
}