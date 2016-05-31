using Leora.Services.Contracts;
using Leora.Services.Mocks;
using Microsoft.Practices.Unity;

namespace Leora.Commands.AspNetWebApi2.Tests
{
    public class UnityConfiguration
    {
        public static IUnityContainer GetContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IFileWriter, MockFileWriter>();
            return container;
        }
    }
}
