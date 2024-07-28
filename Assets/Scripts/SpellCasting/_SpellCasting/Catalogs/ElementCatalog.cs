using System.Collections.Generic;
using UnityEngine;

namespace SpellCasting
{
    public class ElementCatalog : Singleton<ElementCatalog>
    {
        public Dictionary<ElementTypeIndex, ElementType> ElementTypesMap = new Dictionary<ElementTypeIndex, ElementType>();
        public Dictionary<string, ElementType> NameToElementMap = new Dictionary<string, ElementType>();

        [SerializeField]
        private List<ElementType> elementTypes = new List<ElementType>();

        public static ElementType TryCombineElements(ElementType element1, ElementType element2)
        {
            for (int i = 0; i < Instance.elementTypes.Count; i++)
            {
                if (Instance.elementTypes[i].IsSecondary && Instance.elementTypes[i].ComponentElements.Contains(element1) && Instance.elementTypes[i].ComponentElements.Contains(element2))
                {
                    return Instance.elementTypes[i];
                }
            }
            return null;
        }

        protected override void HandleAdditionalInstance()
        {

        }

        public void InitWithMainGame()
        {
            Instance = this;
            for (int i = 0; i < elementTypes.Count; i++)
            {
                ElementTypesMap.Add(elementTypes[i].Index, elementTypes[i]);
                NameToElementMap.Add(elementTypes[i].name, elementTypes[i]);
            }
        }
    }
}