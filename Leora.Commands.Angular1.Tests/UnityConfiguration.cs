using Microsoft.Practices.Unity;

namespace Leora.Commands.Angular1.Tests
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
