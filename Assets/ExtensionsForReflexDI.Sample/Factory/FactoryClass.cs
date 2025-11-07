using ExtensionsForReflexDI.Factory;
using Reflex.Core;
using UnityEngine;

namespace ExtensionsForReflexDI.Sample.Factory
{
    public interface IFactoryClassFactory : IFactory<IFactoryClass, int>
    { }

    public class FactoryClass : IFactoryClass
    {
        public class Factory : CustomFactory<FactoryClass, IFactoryClass, int>, IFactoryClassFactory
        {
            public Factory(Container container) : base(container)
            { }
        }

        public FactoryClass(int param, IInjectableInterface injectable)
        {
            Debug.Log($"int param value is: {param}");
            Debug.Log($"injectable param value is: {injectable}");
        }
    }
}