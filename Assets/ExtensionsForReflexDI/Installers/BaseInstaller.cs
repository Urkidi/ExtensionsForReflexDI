using System;
using Reflex.Core;

namespace ExtensionsForReflexDI.Installers
{
    public abstract class BaseInstaller<T> where T : BaseInstaller<T>
    {
        protected ContainerBuilder ContainerBuilder;

        public static void Install(ContainerBuilder builder)
        {
            var installer = Activator.CreateInstance<T>();
            installer.ContainerBuilder = builder;
            installer.InstallBindings();
        }

        protected abstract void InstallBindings();
    }
    
}


