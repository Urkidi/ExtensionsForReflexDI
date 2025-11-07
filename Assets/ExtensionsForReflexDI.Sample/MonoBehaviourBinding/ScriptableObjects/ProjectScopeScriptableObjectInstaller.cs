using ExtensionsForReflexDI.Installers;
using UnityEngine;

namespace ExtensionsForReflexDI.Sample.MonoBehaviourBinding.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(ProjectScopeScriptableObjectInstaller), menuName = "ScriptableObjects/"+ nameof(ProjectScopeScriptableObjectInstaller), order = 1)] 
    public sealed class ProjectScopeScriptableObjectInstaller : ScriptableObjectInstaller 
    {
        [SerializeField] private SampleConfig _sampleConfig;
        [SerializeField] private SampleConfig2 _sampleConfig2;
        protected override void InstallBindings()
        {
            BindConfig<ISampleConfig>(_sampleConfig);
            BindConfig<ISampleConfig2>(_sampleConfig2);
        }
    }
}