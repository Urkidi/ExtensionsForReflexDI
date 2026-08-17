using ExtensionsForReflexDI.Installers;
using ExtensionsForReflexDI.Sample.Factory;
using ExtensionsForReflexDI.Sample.MonoBehaviourBinding.ScriptableObjects;
using Reflex.Core;
using Reflex.Enums;
using UnityEngine;
using Resolution = Reflex.Enums.Resolution;

namespace ExtensionsForReflexDI.Sample
{
    public class SceneInstallation : MonoBehaviour, IInstaller
    {
        [SerializeField]
        private PrefabInstaller _installer;
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            //Prefabs
            _installer.Install(containerBuilder);
            
            //Factories / Base Installer usage
            FactorySampleInstaller.InstallBindings(containerBuilder);
            containerBuilder.RegisterType<FactoryClassUsage>().NonLazy<FactoryClassUsage>();
            containerBuilder.RegisterType<InjectableClass, IInjectableInterface>();
            
            //ScriptableObjects
            containerBuilder.RegisterType<SampleConfigRequester>(Lifetime.Singleton, Resolution.Eager);
        }
    }
}