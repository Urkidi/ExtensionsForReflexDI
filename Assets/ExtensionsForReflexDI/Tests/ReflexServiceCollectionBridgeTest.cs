using System;
using System.Linq;
using ExtensionsForReflexDI.Tests.Mock;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Reflex.Core;
using Reflex.Exceptions;

namespace ExtensionsForReflexDI.Tests
{
    [TestFixture]
    public sealed class ReflexServiceCollectionBridgeTest
    {
        private readonly ServiceCollection _serviceCollection = new();

        [TearDown]
        public void TearDown()
        {
            _serviceCollection.Clear();
        }

        [Test]
        public void Unsupported_KeyedBinding_ThrowsException()
        {
            _serviceCollection.AddKeyedSingleton("key", typeof(MockedClass));
            ContainerBuilder builder = new();

            Assert.Throws<NotImplementedException>(() => builder.AddFromServiceCollection(_serviceCollection));
        }

        [Test]
        [TestCase(typeof(IMockInterface1))]
        [TestCase(typeof(IMockInterface2))]
        [TestCase(typeof(IMockInterface3))]
        public void SingleType_SingletonBinding_ResolvesForType(Type type)
        {
            _serviceCollection.AddSingleton(type, typeof(MockedClass));
            ContainerBuilder builder = new();
            Assert.DoesNotThrow(() => builder.AddFromServiceCollection(_serviceCollection));

            var container = builder.Build();
            Assert.That(container.Resolve(type), Is.InstanceOf<MockedClass>());
        }

        [Test]
        [TestCase(typeof(IMockInterface1), typeof(IMockInterface3))]
        [TestCase(typeof(IMockInterface1), typeof(IMockInterface2))]
        [TestCase(typeof(IMockInterface2), typeof(IMockInterface3))]
        [TestCase(typeof(IMockInterface1), typeof(IMockInterface2), typeof(IMockInterface3))]
        [TestCase(typeof(IMockInterface3), typeof(IMockInterface2), typeof(IMockInterface1))]
        public void MultipleType_SingletonBinding_ResolvesForContracts(params Type[] resolvedTypes)
        {
            ContainerBuilder builder = new();
            foreach (var resolvedType in resolvedTypes)
                _serviceCollection.AddTransient(resolvedType, typeof(MockedClass));
            
            builder.AddFromServiceCollection(_serviceCollection);
            var container = builder.Build();
            var mocked = resolvedTypes.Select(type => container.Resolve(type)).ToList();
            Assert.That(mocked.All(item => ReferenceEquals(mocked[0], item)), Is.True);
        }

        
        [Test]
        [TestCase(typeof(IMockInterface1), typeof(IMockInterface3))]
        [TestCase(typeof(IMockInterface1), typeof(IMockInterface2))]
        [TestCase(typeof(IMockInterface2), typeof(IMockInterface3))]
        [TestCase(typeof(IMockInterface1), typeof(IMockInterface2), typeof(IMockInterface3))]
        [TestCase(typeof(IMockInterface3), typeof(IMockInterface2), typeof(IMockInterface1))]
        public void MultipleType_TransientBinding_ResolvesForContracts(params Type[] resolvedTypes)
        {
            
            ContainerBuilder builder = new();
            foreach (var resolvedType in resolvedTypes)
                _serviceCollection.AddTransient(resolvedType, typeof(MockedClass));
            
            builder.AddFromServiceCollection(_serviceCollection);
            var container = builder.Build();
            var mocked = resolvedTypes.Select(type => container.Resolve(type)).ToList();
            Assert.That(mocked.All(item => ReferenceEquals(mocked[0], item)), Is.False);
        }

        [Test]
        [TestCase(typeof(IMockInterface1), typeof(IMockInterface2))]
        [TestCase(typeof(IMockInterface2), typeof(IMockInterface3))]
        [TestCase(typeof(IMockInterface1), typeof(IMockInterface2), typeof(IMockInterface3))]
        [TestCase(typeof(IMockInterface3), typeof(IMockInterface2), typeof(IMockInterface1))]
        public void MultipleType_ScopedBinding_ResolvesForContracts(params Type[] resolvedTypes)
        {
            ContainerBuilder builder = new();
            
            foreach (var resolvedType in resolvedTypes)
                _serviceCollection.AddScoped(resolvedType, typeof(MockedClass));
            
            builder.AddFromServiceCollection(_serviceCollection);
            
            var container = builder.Build();
            var scope = container.Scope();
            
            var mocked = resolvedTypes.Select(type => scope.Resolve(type)).ToList();
            Assert.That(mocked.All(item => ReferenceEquals(mocked[0], item)), Is.True);
            
            var mockedParent = resolvedTypes.Select(type => scope.Resolve(type)).ToList();
            Assert.That(mockedParent.All(item => ReferenceEquals(mockedParent[0], item)), Is.True);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-23)]
        [TestCase(30000)]
        [TestCase(98)]
        public void Value_SingletonBinding_ResolvesForSelf(int value)
        {
            var instance = new MockedClass()
            {
                Value = value
            };

            ContainerBuilder builder = new();

            _serviceCollection.AddSingleton(instance);
            builder.AddFromServiceCollection(_serviceCollection);

            var container = builder.Build();
            Assert.That(container.Resolve<MockedClass>().Value, Is.EqualTo(value));
        }

        [Test]
        [TestCase(0, typeof(IMockInterface2))]
        [TestCase(1, typeof(IMockInterface1))]
        [TestCase(-23, typeof(IMockInterface3))]
        [TestCase(30000, typeof(IMockInterface2))]
        [TestCase(98, typeof(IMockInterface1))]
        public void Value_SingletonBinding_ResolvesForContract(int value, Type type)
        {
            var instance = new MockedClass()
            {
                Value = value
            };

            Assume.That(type.IsAssignableFrom(instance.GetType()));

            ContainerBuilder builder = new();

            _serviceCollection.AddSingleton(type, instance);
            builder.AddFromServiceCollection(_serviceCollection);

            var container = builder.Build();
            Assert.That(((MockedClass)container.Resolve(type)).Value, Is.EqualTo(value));
        }

        // TODO Not supported
        // [Test]
        // public void MultipleFactory_SingletonBinding_ResolvesForContracts()
        // {
        //     
        // }
        //
        // [Test]
        // public void MultipleFactory_TransientBinding_ResolvesForContracts()
        // {
        //     ContainerBuilder builder = new();
        //     builder.AddTransient(_=>new MockedClass(), typeof(IMockInterface1));
        //     builder.AddTransient(_=>new MockedClass(), typeof(IMockInterface2));
        //     builder.Build();
        // }
        //
        // [Test]
        // [TestCase(0,typeof(IMockInterface1))]
        // [TestCase(90,typeof(IMockInterface2))]
        // [TestCase(62,typeof(IMockInterface1), typeof(IMockInterface2))]
        // [TestCase(11,typeof(IMockInterface2), typeof(IMockInterface3))]
        // [TestCase(02893,typeof(IMockInterface1), typeof(IMockInterface2), typeof(IMockInterface3))]
        // [TestCase(1234553,typeof(IMockInterface3), typeof(IMockInterface2), typeof(IMockInterface1))]
        // public void MultipleFactory_ScopedBinding_ResolvesForContracts(int value, params Type[] resolvedTypes)
        // {
        //     ContainerBuilder  builder = new();
        //     var container = builder.Build();
        //     using var scope = container.Scope(scopedBuilder =>
        //     {
        //         foreach (var type in resolvedTypes)
        //             _serviceCollection.AddScoped(type, _ => new MockedClass(){Value = value});
        //     
        //         scopedBuilder.AddFromServiceCollection(_serviceCollection);
        //     
        //         var scopedContainer = scopedBuilder.Build();
        //         foreach (var type in resolvedTypes)
        //         {
        //             var item1 = scopedContainer.Resolve(type);
        //             var item2 = scopedContainer.Resolve(type);
        //             Assert.That(((MockedClass)container.Resolve(type)).Value, Is.EqualTo(value));
        //             Assert.That(item1.Equals(item2), Is.Not.True);
        //         
        //         }
        //     });
        //     //Assert.Throws<ContractDefinitionException>(()=>container.Resolve<MockedClass>());
        // }
    }
}