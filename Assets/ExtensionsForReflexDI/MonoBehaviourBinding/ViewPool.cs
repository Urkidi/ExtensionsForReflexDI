using System.Collections.Generic;
using UnityEngine;

namespace ExtensionsForReflexDI.MonoBehaviourBinding
{
    public class ViewPool<T> : IViewPool<T> where T: MonoBehaviour
    {
        private readonly T _prefab;

        private readonly Queue<T> _availableItems = new();

        public ViewPool(T prefab)
        {
            _prefab = prefab;
        }
        
        public void SetInitialSize(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var item = Object.Instantiate(_prefab);
                item.gameObject.SetActive(false);
                _availableItems.Enqueue(item);
            }
        }
        
        public T Spawn(Transform parent)
        {
            T item;
            
            if (_availableItems.Count > 0)
            {
                item = _availableItems.Dequeue();
                item.transform.parent = parent;
            }
            else
                item = Object.Instantiate(_prefab, parent);
            
            item.gameObject.SetActive(true);
            
            return item;
        }

        public void Return(T item)
        {
            item.gameObject.SetActive(false);
            _availableItems.Enqueue(item);
        }
    }
}