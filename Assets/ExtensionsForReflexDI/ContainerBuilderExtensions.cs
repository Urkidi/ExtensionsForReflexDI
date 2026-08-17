using System;
using Reflex.Core;
using Reflex.Enums;

namespace ExtensionsForReflexDI
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder NonLazy<T>(this ContainerBuilder builder)
        {
            Action<Container> resolve = null;

            resolve = (container) =>
            {
                container.Resolve<T>();
                builder.OnContainerBuilt -= resolve;
            };

            builder.OnContainerBuilt += resolve;
            return builder;
        }

        //Singleton

        #region Singleton

        public static ContainerBuilder RegisterType<T>(this ContainerBuilder builder,
            Lifetime lifeTime = Lifetime.Singleton, Resolution resolution = Resolution.Lazy)
        {
            builder.RegisterType(typeof(T), lifeTime, resolution);
            return builder;
        }

        public static ContainerBuilder RegisterType<T, TInterface>(this ContainerBuilder builder,
            Lifetime lifeTime = Lifetime.Singleton, Resolution resolution = Resolution.Lazy)
            where T : TInterface
        {
            var contracts = new[] { typeof(TInterface) };
            builder.RegisterType(typeof(T), contracts, lifeTime, resolution);
            return builder;
        }

        public static ContainerBuilder RegisterType<T, TInterface1, TInterface2>(this ContainerBuilder builder,
            Lifetime lifeTime = Lifetime.Singleton, Resolution resolution = Resolution.Lazy)
            where T : TInterface1, TInterface2
        {
            var contracts = new[] { typeof(TInterface1), typeof(TInterface2) };
            builder.RegisterType(typeof(T), contracts, lifeTime, resolution);
            return builder;
        }

        public static ContainerBuilder RegisterType<T, TInterface1, TInterface2, TInterface3>(
            this ContainerBuilder builder, Lifetime lifeTime = Lifetime.Singleton,
            Resolution resolution = Resolution.Lazy)
            where T : TInterface1, TInterface2, TInterface3
        {
            var contracts = new[] { typeof(TInterface1), typeof(TInterface2), typeof(TInterface3) };
            builder.RegisterType(typeof(T), contracts, lifeTime, resolution);
            return builder;
        }

        public static ContainerBuilder RegisterType<T, TInterface1, TInterface2, TInterface3, TInterface4>(
            this ContainerBuilder builder, Lifetime lifeTime = Lifetime.Singleton,
            Resolution resolution = Resolution.Lazy)
            where T : TInterface1, TInterface2, TInterface3, TInterface4
        {
            var contracts = new[]
                { typeof(TInterface1), typeof(TInterface2), typeof(TInterface3), typeof(TInterface4) };
            builder.RegisterType(typeof(T), contracts, lifeTime, resolution);
            return builder;
        }

        public static ContainerBuilder RegisterType<T, TInterface1, TInterface2, TInterface3, TInterface4, TInterface5>(
            this ContainerBuilder builder, Lifetime lifeTime = Lifetime.Singleton,
            Resolution resolution = Resolution.Lazy)
            where T : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5
        {
            var contracts = new[]
            {
                typeof(TInterface1), typeof(TInterface2), typeof(TInterface3), typeof(TInterface4), typeof(TInterface5)
            };
            builder.RegisterType(typeof(T), contracts, lifeTime, resolution);
            return builder;
        }


        public static ContainerBuilder RegisterType<T, TInterface1, TInterface2, TInterface3, TInterface4, TInterface5,
            TInterface6>(
            this ContainerBuilder builder, Lifetime lifeTime = Lifetime.Singleton,
            Resolution resolution = Resolution.Lazy)
            where T : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6
        {
            var contracts = new[]
            {
                typeof(TInterface1), typeof(TInterface2), typeof(TInterface3), typeof(TInterface4), typeof(TInterface5),
                typeof(TInterface6)
            };
            builder.RegisterType(typeof(T), contracts, lifeTime, resolution);
            return builder;
        }


        public static ContainerBuilder RegisterType<T, TInterface1, TInterface2, TInterface3, TInterface4, TInterface5,
            TInterface6, TInterface7>(
            this ContainerBuilder builder, Lifetime lifeTime = Lifetime.Singleton,
            Resolution resolution = Resolution.Lazy)
            where T : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7
        {
            var contracts = new[]
            {
                typeof(TInterface1), typeof(TInterface2), typeof(TInterface3), typeof(TInterface4), typeof(TInterface5),
                typeof(TInterface6), typeof(TInterface7)
            };
            builder.RegisterType(typeof(T), contracts, lifeTime, resolution);
            return builder;
        }

        #endregion
    }
}