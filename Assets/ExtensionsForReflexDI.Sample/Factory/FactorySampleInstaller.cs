using JetBrains.Annotations;
using Reflex.Core;

namespace ExtensionsForReflexDI.Sample.Factory
{
    [UsedImplicitly]
    public static class FactorySampleInstaller
    {
        public static void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(typeof(FactoryClass.Factory), typeof(IFactoryClassFactory));
            builder.AddSingleton(typeof(FactoryClassUsage));
        }
    }
}