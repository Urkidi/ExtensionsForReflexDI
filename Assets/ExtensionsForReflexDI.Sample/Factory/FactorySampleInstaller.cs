using ExtensionsForReflexDI.Installers;

namespace ExtensionsForReflexDI.Sample.Factory
{
    public class FactorySampleInstaller : BaseInstaller<FactorySampleInstaller>
    {
        protected override void InstallBindings()
        {
            ContainerBuilder.AddSingleton(typeof(FactoryClass.Factory), typeof(IFactoryClassFactory));
            ContainerBuilder.AddSingleton(typeof(FactoryClassUsage));
            
        }
    }
}