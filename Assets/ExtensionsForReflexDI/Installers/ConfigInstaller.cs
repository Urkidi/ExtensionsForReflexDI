using UnityEngine;

namespace ExtensionsForReflexDI.Installers
{
    public abstract class ConfigInstaller : ScriptableObjectInstaller
    {

        protected void BindConfig<TInterface>(ScriptableObject config)
        {
            ContainerBuilder.AddSingleton(config, typeof(TInterface));
        }
    }
}