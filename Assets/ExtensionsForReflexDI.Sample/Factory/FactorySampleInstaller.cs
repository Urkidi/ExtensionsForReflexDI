using JetBrains.Annotations;
using Reflex.Core;
using Reflex.Enums;

namespace ExtensionsForReflexDI.Sample.Factory
{
    [UsedImplicitly]
    public static class FactorySampleInstaller
    {
        public static void InstallBindings(ContainerBuilder builder)
        {
            builder.RegisterType<FactoryClass.Factory, IFactoryClassFactory>();
            builder.RegisterType<FactoryClassUsage>(Lifetime.Singleton, Resolution.Eager);
        }
    }
}