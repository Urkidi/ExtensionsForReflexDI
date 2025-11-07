using ExtensionsForReflexDI.Sample.Factory;
using Reflex.Core;
using UnityEngine;

namespace ExtensionsForReflexDI.Sample
{
    public class Installation : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(typeof(InjectableClass),  typeof(IInjectableInterface));
            FactorySampleInstaller.Install(containerBuilder);
        }
    }
}