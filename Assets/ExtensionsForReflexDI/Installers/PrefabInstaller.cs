using System;
using ExtensionsForReflexDI.MonoBehaviourBinding;
using Reflex.Core;
using UnityEngine;

namespace ExtensionsForReflexDI.Installers
{
    public abstract class PrefabInstaller : ScriptableObject
    {
        protected ContainerBuilder ContainerBuilder;
        
        public void Install(ContainerBuilder builder)
        {
            ContainerBuilder = builder;
            InstallBindings();
        }

        protected abstract void InstallBindings();

        protected void BindViewFactory<T>(T prefab) where T : MonoBehaviour
        {
            ContainerBuilder.AddSingleton<IViewFactory<T>>(_ => new ViewFactory<T>(prefab));
        }
        
        protected void BindViewPool<T>(T prefab) where T : MonoBehaviour
        {
            ContainerBuilder.AddSingleton(container => 
                    new ViewPool<T>(prefab),
                typeof(ViewPool<T>), typeof(IViewPool<T>));
        }
        
        protected void BindViewPool<T>(T prefab, int initialSize) where T : MonoBehaviour
        {
            BindViewPool(prefab);
            Action<Container> setInitialSize = null;
            setInitialSize = (container) =>
            {
                
                container.Resolve<ViewPool<T>>().SetInitialSize(initialSize);
                ContainerBuilder.OnContainerBuilt -= setInitialSize;
            };
            
            ContainerBuilder.OnContainerBuilt += setInitialSize;
        }

    }
}