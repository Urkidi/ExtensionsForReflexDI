using System;
using Reflex.Core;

namespace ExtensionsForReflexDI.Installers
{
    /// <summary>
    /// The main purpose of the Base Installer is to organize bindings by feature.
    /// BaseInstaller only requires you to implement it with T = to the class that inherits it.
    /// Once done, you can statically call T.Install() type to install all bindings in the chosen scope installer.
    /// </summary>
    /// <typeparam name="T">The class that is inheriting</typeparam>
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


