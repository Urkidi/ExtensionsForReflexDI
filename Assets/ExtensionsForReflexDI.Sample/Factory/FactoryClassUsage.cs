namespace ExtensionsForReflexDI.Sample.Factory
{
    public sealed class FactoryClassUsage
    {
        public FactoryClassUsage(IFactoryClassFactory factoryClassFactory)
        {
            var factoryItem = factoryClassFactory.Create(1);
            var factoryItem2 = factoryClassFactory.Create(29);
        }
    }
}