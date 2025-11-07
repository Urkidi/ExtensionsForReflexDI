using ExtensionsForReflexDI.Installers;
using Reflex.Core;
using UnityEngine;

namespace ExtensionsForReflexDI.Sample
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private ScriptableObjectInstaller _scriptableObjectInstaller;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            _scriptableObjectInstaller.Install(containerBuilder);
        }
    }
}