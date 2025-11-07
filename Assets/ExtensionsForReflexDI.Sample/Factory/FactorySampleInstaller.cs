using ExtensionsForReflexDI.Installers;
using JetBrains.Annotations;

namespace ExtensionsForReflexDI.Sample.Factory
{
    [UsedImplicitly]
    public sealed class FactorySampleInstaller : BaseInstaller<FactorySampleInstaller> 
    {
        protected override void InstallBindings()
        {
            ContainerBuilder.AddSingleton(typeof(FactoryClass.Factory), typeof(IFactoryClassFactory));
            ContainerBuilder.AddSingleton(typeof(FactoryClassUsage));
            
        }
    }
}