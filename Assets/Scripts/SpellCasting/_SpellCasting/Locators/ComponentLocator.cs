using System.Collections.Generic;
using UnityEngine;

namespace SpellCasting
{
    public class ComponentLocator<T> : MonoBehaviour where T: Component
    {
        [SerializeField]
        protected T[] componentList;

        public T[] ComponentList { get => componentList; }

        protected Dictionary<string, T> nameToComponent = new Dictionary<string, T>();

        protected virtual void Awake()
        {
            for (int i = 0; i < componentList.Length; i++)
            {
                AddChild(componentList[i]);
            }
        }

        public void AddChild(T component)
        {
            if (nameToComponent.ContainsKey(component.name))
            {
                Debug.LogError($"child with the name {component.name} already exists. multiple children with the same name are not supported");
                return;
            }
            nameToComponent[component.name] = component;
        }

        public virtual T LocateByName(string name)
        {
            if (nameToComponent.ContainsKey(name))
            {
                return nameToComponent[name];
            }
            Debug.LogError($"could not find child with the name {name}", this);
            return null;
        }

        public virtual GameObject LocateByNameGameObject(string name)
        {
            if (nameToComponent.ContainsKey(name))
            {
                return nameToComponent[name].gameObject;
            }
            Debug.LogError($"could not find child with the name {name}", this);
            return null;
        }
    }
}