using ExtensionsForReflexDI.Installers;
using UnityEngine;

namespace ExtensionsForReflexDI.Sample.MonoBehaviourBinding.Prefabs
{
    [CreateAssetMenu(fileName = nameof(SceneScopePrefabInstaller), menuName = "Prefabs/Installations"+ nameof(SceneScopePrefabInstaller), order = 1)]
    public sealed class SceneScopePrefabInstaller : PrefabInstaller
    {
        [SerializeField] private SpawnableItem spawnableItem;
        protected override void InstallBindings()
        {
            BindViewFactory(spawnableItem);
            BindViewPool(spawnableItem);
        }
    }
}