using System.Collections.Generic;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace c3o.Web.Site.Data
{
    //interface IModuleInitializer
    //{
    //    void Initialize(IUnityContainer container);
    //}

    //public abstract class ModuleInitializer : IModuleInitializer
    //{
    //    public virtual void Initialize(IUnityContainer container)
    //    {
    //    }
    //}

    public static class Container
    {
        private static readonly object syncRoot = new object();
        private static IUnityContainer container;

        public static IUnityContainer Current
        {
            get
            {
                // use your favorite singleton pattern here
                if (container == null)
                {
                    lock (syncRoot)
                    {
                        if (container == null)
                        {
                            container = CreateContainer();
                        }
                    }
                }
                return container;
            }
        }

        private static IUnityContainer CreateContainer()
        {
            IUnityContainer container = new UnityContainer();

            UnityConfigurationSection config = ConfigurationManager.GetSection("unity") as UnityConfigurationSection;
            //if (config == null) return container;

            if (config == null || config.Containers.Default == null) return container;
            
            //Microsoft.Practices.Unity.Configuration.ContainerElement.Configure(Microsoft.Practices.Unity.IUnityContainer)' is obsolete: 'Use the UnityConfigurationSection.Configure(container, name) method instead'
            //
            //config.Containers.Default.Configure(container);

            config.Configure(container);
                       

            //IEnumerable<IModuleInitializer> initializers = container.ResolveAll<IModuleInitializer>();

            //foreach (IModuleInitializer initializer in initializers)
            //{
            //    initializer.Initialize(container);
            //}

            return container;
        }
    }
}
