using ExtensionsForReflexDI.Installers;
using ExtensionsForReflexDI.Sample.Factory;
using ExtensionsForReflexDI.Sample.MonoBehaviourBinding.ScriptableObjects;
using Reflex.Core;
using UnityEngine;

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
            containerBuilder.AddSingleton<FactoryClassUsage>().NonLazy<FactoryClassUsage>();
            containerBuilder.AddSingleton<InjectableClass, IInjectableInterface>();
            
            //ScriptableObjects
            containerBuilder.AddSingleton<SampleConfigRequester>().NonLazy<SampleConfigRequester>();
        }
    }
}