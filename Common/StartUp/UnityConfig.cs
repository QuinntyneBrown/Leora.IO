using System.Web.Http;
using Microsoft.Practices.Unity;
using Unity.WebApi;

namespace Common.StartUp
{
    public abstract class UnityConfig
    {
        public static void RegisterComponents(UnityContainer container = null)
        {
            if (container == null)
            {
                container = new UnityContainer();
            }

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

    }
}
