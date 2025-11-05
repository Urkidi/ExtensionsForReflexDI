using UnityEngine;

namespace ExtensionsForReflexDI.MonoBehaviourBinding
{
    public class ViewFactory<T> : IViewFactory<T> where T : MonoBehaviour
    {
        private readonly T _prefab;

        public ViewFactory(T prefab)
        {
            _prefab = prefab;
        }

        public T Create(Transform parent)
        {
            return Object.Instantiate(_prefab, parent);
        }
    }
}