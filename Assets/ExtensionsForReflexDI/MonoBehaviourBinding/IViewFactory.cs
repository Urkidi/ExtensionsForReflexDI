using UnityEngine;

namespace ExtensionsForReflexDI.MonoBehaviourBinding
{
    public interface IViewFactory<T> where T: MonoBehaviour
    {
        T Create(Transform parent);
    }
}