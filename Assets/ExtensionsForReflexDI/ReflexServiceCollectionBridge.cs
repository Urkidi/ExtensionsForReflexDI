using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;
using Reflex.Core;
using UnityEngine;

namespace ExtensionsForReflexDI
{
    public static class ReflexServiceCollectionBridge
    {
        /// <summary>
        /// Translates IServiceCollection services to Reflex bindings in the given containerbuilder
        /// Note that reflex does not support the keyed services natively.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="services"></param>
        public static void AddFromServiceCollection(this ContainerBuilder builder, IServiceCollection services)
        {
            Dictionary<Type, List<ServiceDescriptor>> groupedTypeDescriptors = new();
            Dictionary<object, List<ServiceDescriptor>> groupedInstanceDescriptors = new();
            List<ServiceDescriptor> groupedFactoryDescriptors = new();

            if(services.Any(service => service.IsKeyedService))
                throw new NotImplementedException("Reflex does not support keyed services natively");
            
            var nonKeyedServices = services.Where(service => !service.IsKeyedService);

            foreach (var sd in nonKeyedServices)
            {
                if (sd.ImplementationInstance != null)
                {
                    if (groupedInstanceDescriptors.ContainsKey(sd.ImplementationInstance))
                        groupedInstanceDescriptors[sd.ImplementationInstance].Add(sd);
                    else
                        groupedInstanceDescriptors[sd.ImplementationInstance] = new List<ServiceDescriptor>() { sd };
                    continue;
                }

                if (sd.ImplementationFactory != null)
                {
                    groupedFactoryDescriptors.Add(sd);
                    continue;
                }

                if (sd.ImplementationType != null)
                {
                    if (groupedTypeDescriptors.ContainsKey(sd.ImplementationType))
                        groupedTypeDescriptors[sd.ImplementationType].Add(sd);
                    else
                        groupedTypeDescriptors[sd.ImplementationType] = new List<ServiceDescriptor>() { sd };
                }
            }

            SetUpTypeBindings(builder, groupedTypeDescriptors);
            SetUpValueBindings(builder, groupedInstanceDescriptors);
            //SetUpFactoryBindings(builder, groupedFactoryDescriptors);
        }

        private static void SetUpTypeBindings(ContainerBuilder containerBuilder,
            Dictionary<Type, List<ServiceDescriptor>> descriptors)
        {
            foreach (var sd in descriptors.Keys)
            {
                var sdList = descriptors[sd];
                if (sdList.Select(item => item.Lifetime).Distinct().Skip(1).Any())
                    throw new Exception("Multiple service descriptors have different lifetimes");

                var lifetime = sdList.First().Lifetime;
                Type[] concretes = sdList.Select(descriptor => descriptor.ServiceType).ToArray();
                switch (lifetime)
                {
                    case ServiceLifetime.Singleton:
                        containerBuilder.AddSingleton(sd, concretes);
                        break;
                    case ServiceLifetime.Scoped:
                        containerBuilder.AddScoped(sd, concretes);
                        break;
                    case ServiceLifetime.Transient:
                        containerBuilder.AddTransient(sd, concretes);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(
                            $"Cannot find a service lifetime of type {lifetime.ToString()}");
                }
            }
        }

        private static void SetUpValueBindings(ContainerBuilder containerBuilder,
            Dictionary<object, List<ServiceDescriptor>> descriptors)
        {
            foreach (var sd in descriptors.Keys)
            {
                var sdList = descriptors[sd];
                if (sdList.Select(item => item.Lifetime).Distinct().Skip(1).Any())
                    throw new Exception("Multiple service descriptors have different lifetimes");

                var lifetime = sdList.First().Lifetime;
                Type[] concretes = sdList.Select(descriptor => descriptor.ServiceType).ToArray();
                switch (lifetime)
                {
                    case ServiceLifetime.Singleton:
                        containerBuilder.AddSingleton(sd, concretes);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"Reflex only supports singleton value binding");
                }
            }
        }

        private static void SetUpFactoryBindings(ContainerBuilder containerBuilder, List<ServiceDescriptor> descriptors)
        {
            //TODO Not able to properly resolve the factories
            
            
            foreach (var descriptor in descriptors)
            {
                switch (descriptor.Lifetime)
                {
                    case ServiceLifetime.Singleton:
                        containerBuilder.AddSingleton(_ => descriptor.ImplementationFactory(null), descriptor.ServiceType);
                        break;
                    case ServiceLifetime.Scoped:
                        containerBuilder.AddScoped(_ => descriptor.ImplementationFactory(null), descriptor.ServiceType);
                        break;
                    case ServiceLifetime.Transient:
                        containerBuilder.AddTransient(_ => descriptor.ImplementationFactory(null), descriptor.ServiceType);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(
                            $"Cannot find a service lifetime of type {descriptor.Lifetime.ToString()}");
                }
            }
        }

        private static object CastTo(object instance, Type type)
        {
            return CasterMaker(type)(instance);
        }

        private static Func<object, object> CasterMaker(Type targetType)
        {
            var param = Expression.Parameter(typeof(object), "input");
            var body = Expression.Convert(Expression.Convert(param, targetType), typeof(object));
            var lambda = Expression.Lambda<Func<object, object>>(body, param);
            return lambda.Compile();
        }
    }
}