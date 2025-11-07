using JetBrains.Annotations;
using UnityEngine;

namespace ExtensionsForReflexDI.Sample.MonoBehaviourBinding.ScriptableObjects
{
    [UsedImplicitly]
    public sealed class SampleConfigRequester
    {
        public SampleConfigRequester(ISampleConfig config)
        {
            Debug.Log($"Config resolved. Value: {config.Value}");
        }
}
}