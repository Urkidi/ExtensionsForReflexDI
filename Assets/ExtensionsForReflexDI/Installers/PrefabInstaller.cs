using System;
using ExtensionsForReflexDI.MonoBehaviourBinding;
using Reflex.Core;
using Reflex.Enums;
using UnityEngine;
using Resolution = Reflex.Enums.Resolution;

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
            ContainerBuilder.RegisterFactory(_ => new ViewFactory<T>(prefab), new[] { typeof(IViewFactory<T>) }, Lifetime.Singleton, Resolution.Lazy);
        }
        
        protected void BindViewPool<T>(T prefab) where T : MonoBehaviour
        {
            ContainerBuilder.RegisterFactory(_ => 
                    new ViewPool<T>(prefab),
                new[] { typeof(ViewPool<T>), typeof(IViewPool<T>) }, Lifetime.Transient, Resolution.Lazy);
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