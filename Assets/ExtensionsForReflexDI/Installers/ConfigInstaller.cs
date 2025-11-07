using Reflex.Core;
using UnityEngine;

namespace ExtensionsForReflexDI.Installers
{
    public abstract class ConfigInstaller : ScriptableObject
    {
        private ContainerBuilder _containerBuilder;
        
        public void Install(ContainerBuilder builder)
        {
            _containerBuilder = builder;
            InstallBindings();
        }

        protected abstract void InstallBindings();
        
        protected void BindConfig<TContract>(ScriptableObject config)
        {
            _containerBuilder.AddSingleton(config, typeof(TContract));
        }
    }
}