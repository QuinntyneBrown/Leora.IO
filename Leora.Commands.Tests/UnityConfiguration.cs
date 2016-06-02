using Leora.Commands.Contracts;
using Leora.Services;
using Leora.Services.Contracts;
using Leora.Services.Mocks;
using Microsoft.Practices.Unity;

namespace Leora.Commands.Tests
{
    public class UnityConfiguration
    {
        public static IUnityContainer GetContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IGenerateCommandCommand, GenerateCommandCommand>();

            container.RegisterType<ITemplateManager, MockTemplateManager>();
            container.RegisterType<ITemplateProcessor, MockTemplateProcessor>();
            container.RegisterType<INamingConventionConverter, NamingConventionConverter>();
            container.RegisterType<IProjectManager, MockProjectManager>();            
            container.RegisterType<IFileWriter, MockFileWriter>();

            return container;
        }
    }
}
