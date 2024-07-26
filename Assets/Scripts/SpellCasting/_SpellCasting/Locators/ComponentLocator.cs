using System.Collections.Generic;
using UnityEngine;

namespace SpellCasting
{
    public class ComponentLocator<T> : MonoBehaviour where T: Component
    {
        [SerializeField]
        private T[] componentList;

        private Dictionary<string, T> _nameToComponent = new Dictionary<string, T>();

        public virtual void Awake()
        {
            for (int i = 0; i < componentList.Length; i++)
            {
                AddChild(componentList[i]);
            }
        }

        public void AddChild(T component)
        {
            if (_nameToComponent.ContainsKey(component.name))
            {
                Debug.LogError($"child with the name {component.name} already exists. multiple children with the same name are not supported");
                return;
            }
            _nameToComponent[component.name] = component;
        }

        public virtual T LocateByName(string name)
        {
            if (_nameToComponent.ContainsKey(name))
            {
                return _nameToComponent[name];
            }
            Debug.LogError($"could not find child with the name {name}", this);
            return null;
        }

        public virtual GameObject LocateByNameGameObject(string name)
        {
            if (_nameToComponent.ContainsKey(name))
            {
                return _nameToComponent[name].gameObject;
            }
            Debug.LogError($"could not find child with the name {name}", this);
            return null;
        }
    }
}