using System;
using Reflex.Core;

namespace ExtensionsForReflexDI
{
    public sealed class ReflexServiceProvider: IServiceProvider
    {
        private readonly ContainerBuilder _containerBuilder;
        private Container _container;

        public ReflexServiceProvider(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder;
            containerBuilder.OnContainerBuilt += GetContainer;
        }

        private void GetContainer(Container obj)
        {
            _containerBuilder.OnContainerBuilt -= GetContainer;
            _container = obj;
        }

        public object GetService(Type serviceType)
        {
            return _container.Single<Type>();
        }
    }
}