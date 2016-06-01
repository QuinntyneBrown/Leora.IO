using Leora.Commands.Angular1.Contracts;
using Leora.Services;
using Leora.Services.Angular1;
using Leora.Services.Angular1.Contracts;
using Leora.Services.Contracts;
using Leora.Services.Mocks;
using Microsoft.Practices.Unity;

namespace Leora.Commands.Angular1.Tests
{
    public class UnityConfiguration
    {
        public static IUnityContainer GetContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IGenerateActionCreatorCommand, GenerateActionCreatorCommand>();
            container.RegisterType<IGenerateComponentCommand, GenerateComponentCommand>();
            container.RegisterType<IGenerateContainerCommand, GenerateContainerCommand>();
            container.RegisterType<IGenerateCrudFeatureCommand, GenerateCrudFeatureCommand>();
            container.RegisterType<IGenerateEditorCommand, GenerateEditorCommand>();
            container.RegisterType<IGenerateFeatureCommand, GenerateFeatureCommand>();
            container.RegisterType<IGenerateListCommand, GenerateListCommand>();
            container.RegisterType<IGenerateModelCommand, GenerateModelCommand>();
            container.RegisterType<IGenerateModuleCommand, GenerateModuleCommand>();
            container.RegisterType<IGeneratePagedListCommand, GeneratePagedListCommand>();
            container.RegisterType<IGenerateServiceCommand, GenerateServiceCommand>();
            container.RegisterType<IGenerateUIComponentCommand, GenerateUIComponentCommand>();


            container.RegisterType<ITemplateManager, MockTemplateManager>();
            container.RegisterType<ITemplateProcessor, MockTemplateProcessor>();
            container.RegisterType<INamingConventionConverter, NamingConventionConverter>();
            container.RegisterType<IProjectManager, MockProjectManager>();
            container.RegisterType<IMainManager, MainManager>();
            container.RegisterType<IFileWriter, MockFileWriter>();

            return container;
        }
    }
}
