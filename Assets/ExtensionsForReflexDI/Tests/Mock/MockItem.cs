using ExtensionsForReflexDI.Factory;
using Reflex.Core;

namespace ExtensionsForReflexDI.Tests.Mock
{
    public interface IMockItemFactory : IFactory<MockItem, int>
    { }

    public class MockItem
    {
        public class MockItemFactory : CustomFactory<MockItem, MockItem, int>, IMockItemFactory
        {
            public MockItemFactory(Container container) : base(container)
            {
            }
        }

        public MockItem(int item)
        {
            
        }
    }
}