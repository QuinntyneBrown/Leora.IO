using Microsoft.Practices.Unity;

namespace Leora.Services.Tests
{
    public class UnityConfiguration
    {
        public static IUnityContainer GetContainer()
        {
            var container = new UnityContainer();

            return container;
        }
    }
}
