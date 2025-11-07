using UnityEngine;

namespace ExtensionsForReflexDI.Sample.MonoBehaviourBinding.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(SampleConfig2), menuName = "ScriptableObjects/Configs/"+ nameof(SampleConfig2))]
    public sealed class SampleConfig2 : ScriptableObject, ISampleConfig2
    {
        [field: SerializeField]
        public string StringValue { get; private set; }
    }
}