using System.Collections.Generic;
using System.Linq;
using ExtensionsForReflexDI.MonoBehaviourBinding;
using Reflex.Attributes;
using UnityEngine;

namespace ExtensionsForReflexDI.Sample.MonoBehaviourBinding.Prefabs
{
    public sealed class TestSpawner : MonoBehaviour
    {
        private IViewPool<SpawnableItem> _viewPool;
        private IViewFactory<SpawnableItem> _viewFactory;

        private readonly Queue<SpawnableItem> _queue = new();

        [Inject]
        public void Inject(IViewPool<SpawnableItem> viewPool, IViewFactory<SpawnableItem> viewFactory)
        {
            _viewPool = viewPool;
            _viewFactory = viewFactory;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _viewFactory.Create(null);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                _queue.Enqueue(_viewPool.Spawn(null));
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (_queue.Any())
                    _viewPool.Return(_queue.Dequeue());
            }
        }
    }
}