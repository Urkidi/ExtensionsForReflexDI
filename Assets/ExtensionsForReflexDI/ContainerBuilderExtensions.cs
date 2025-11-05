using System;
using Reflex.Core;

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

        public static ContainerBuilder AddSingleton<T>(this ContainerBuilder builder)
        {
            builder.AddSingleton(typeof(T));
            return builder;
        }

        public static ContainerBuilder AddSingleton<T, TInterface>(this ContainerBuilder builder)
            where T : TInterface
        {
            builder.AddSingleton(typeof(T), typeof(TInterface));
            return builder;
        }

        public static ContainerBuilder AddSingleton<T, TInterface1, TInterface2>(this ContainerBuilder builder)
            where T : TInterface1, TInterface2
        {
            builder.AddSingleton(typeof(T), typeof(TInterface1), typeof(TInterface2));
            return builder;
        }

        public static ContainerBuilder AddSingleton<T, TInterface1, TInterface2, TInterface3>(
            this ContainerBuilder builder)
            where T : TInterface1, TInterface2, TInterface3
        {
            builder.AddSingleton(typeof(T), typeof(TInterface1), typeof(TInterface2), typeof(TInterface3));
            return builder;
        }

        public static ContainerBuilder AddSingleton<T, TInterface1, TInterface2, TInterface3, TInterface4>(
            this ContainerBuilder builder)
            where T : TInterface1, TInterface2, TInterface3, TInterface4
        {
            builder.AddSingleton(typeof(T), typeof(TInterface1), typeof(TInterface2), typeof(TInterface3),
                typeof(TInterface4));
            return builder;
        }

        public static ContainerBuilder AddSingleton<T, TInterface1, TInterface2, TInterface3, TInterface4, TInterface5>(
            this ContainerBuilder builder)
            where T : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5
        {
            builder.AddSingleton(typeof(T), typeof(TInterface1), typeof(TInterface2), typeof(TInterface3),
                typeof(TInterface4), typeof(TInterface5));
            return builder;
        }


        public static ContainerBuilder AddSingleton<T, TInterface1, TInterface2, TInterface3, TInterface4, TInterface5,
            TInterface6>(
            this ContainerBuilder builder)
            where T : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6
        {
            builder.AddSingleton(typeof(T), typeof(TInterface1), typeof(TInterface2), typeof(TInterface3),
                typeof(TInterface4), typeof(TInterface5), typeof(TInterface6));
            return builder;
        }


        public static ContainerBuilder AddSingleton<T, TInterface1, TInterface2, TInterface3, TInterface4, TInterface5,
            TInterface6, TInterface7>(
            this ContainerBuilder builder)
            where T : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7
        {
            builder.AddSingleton(typeof(T), typeof(TInterface1), typeof(TInterface2), typeof(TInterface3),
                typeof(TInterface4), typeof(TInterface5), typeof(TInterface6), typeof(TInterface7));
            return builder;
        }

        #endregion

        //Transient

        #region Transient
        
        public static ContainerBuilder AddTransient<T>(this ContainerBuilder builder)
        {
            builder.AddTransient(typeof(T));
            return builder;
        }

        public static ContainerBuilder AddTransient<T, TInterface>(this ContainerBuilder builder)
            where T : TInterface
        {
            builder.AddTransient(typeof(T), typeof(TInterface));
            return builder;
        }

        public static ContainerBuilder AddTransient<T, TInterface1, TInterface2>(this ContainerBuilder builder)
            where T : TInterface1, TInterface2
        {
            builder.AddTransient(typeof(T), typeof(TInterface1), typeof(TInterface2));
            return builder;
        }

        public static ContainerBuilder AddTransient<T, TInterface1, TInterface2, TInterface3, TInterface4>(
            this ContainerBuilder builder)
            where T : TInterface1, TInterface2, TInterface3, TInterface4
        {
            builder.AddTransient(typeof(T), typeof(TInterface1), typeof(TInterface2), typeof(TInterface3),
                typeof(TInterface4));
            return builder;
        }

        public static ContainerBuilder AddTransient<T, TInterface1, TInterface2, TInterface3, TInterface4, TInterface5>(
            this ContainerBuilder builder)
            where T : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5
        {
            builder.AddTransient(typeof(T), typeof(TInterface1), typeof(TInterface2), typeof(TInterface3),
                typeof(TInterface4), typeof(TInterface5));
            return builder;
        }

        public static ContainerBuilder AddTransient<T, TInterface1, TInterface2, TInterface3, TInterface4, TInterface5,
            TInterface6>(
            this ContainerBuilder builder)
            where T : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6
        {
            builder.AddTransient(typeof(T), typeof(TInterface1), typeof(TInterface2), typeof(TInterface3),
                typeof(TInterface4), typeof(TInterface5), typeof(TInterface6));
            return builder;
        }

        public static ContainerBuilder AddTransient<T, TInterface1, TInterface2, TInterface3, TInterface4, TInterface5,
            TInterface6, TInterface7>(
            this ContainerBuilder builder)
            where T : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7
        {
            builder.AddTransient(typeof(T), typeof(TInterface1), typeof(TInterface2), typeof(TInterface3),
                typeof(TInterface4), typeof(TInterface5), typeof(TInterface6), typeof(TInterface7));
            return builder;
        }

        #endregion
    }
}