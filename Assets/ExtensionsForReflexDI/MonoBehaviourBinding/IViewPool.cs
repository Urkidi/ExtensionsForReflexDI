using UnityEngine;

namespace ExtensionsForReflexDI.MonoBehaviourBinding
{
    public interface IViewPool<T> where T: MonoBehaviour
    {
        T Spawn(Transform parent);
        void Return(T item);
    }
}