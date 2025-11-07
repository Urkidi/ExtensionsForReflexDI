using ExtensionsForReflexDI.Factory;
using JetBrains.Annotations;
using Reflex.Core;
using UnityEngine;

namespace ExtensionsForReflexDI.Sample.Factory
{
    public interface IFactoryClassFactory : IFactory<IFactoryClass, int>
    { }

    [UsedImplicitly]
    public sealed class FactoryClass : IFactoryClass
    {
        public class Factory : CustomFactory<FactoryClass, IFactoryClass, int>, IFactoryClassFactory
        {
            public Factory(Container container) : base(container)
            { }
        }

        public FactoryClass(int param, IInjectableInterface injectable)
        {
            Debug.Log($"{nameof(FactoryClass)} Instance: int param value is: {param}");
            Debug.Log($"{nameof(FactoryClass)} Instance: Injectable param is: {injectable}");
        }
    }
}