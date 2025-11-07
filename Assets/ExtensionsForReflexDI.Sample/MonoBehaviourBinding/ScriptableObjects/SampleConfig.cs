using UnityEngine;

namespace ExtensionsForReflexDI.Sample.MonoBehaviourBinding.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(SampleConfig), menuName = "ScriptableObjects/Configs/"+ nameof(SampleConfig), order = 1)]
    public sealed class SampleConfig : ScriptableObject, ISampleConfig
    {
        [field: SerializeField]
        public int Value { get; private set; }
    }
}