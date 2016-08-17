using Leora.Commands;
using Leora.Commands.Angular1;
using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Contracts;
using Leora.Commands.VisualStudio;
using Leora.Commands.VisualStudio.Contracts;
using Leora.Services;
using Leora.Services.Angular1;
using Leora.Services.Angular1.Contracts;
using Leora.Services.Contracts;
using Microsoft.Practices.Unity;

namespace Leora.Cli
{
    public class UnityConfiguration
    {
        public static IUnityContainer GetContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<INamingConventionConverter, NamingConventionConverter>();
            container.RegisterType<ITemplateManager, TemplateManager>();
            container.RegisterType<ITemplateProcessor, TemplateProcessor>();
            container.RegisterType<IDotNetTemplateProcessor, DotNetTemplateProcessor>();
            container.RegisterType<IProjectManager, ProjectManager>();
            container.RegisterType<IFileWriter, FileWriter>();

            container.RegisterType<IGenerateActionCreatorCommand, GenerateActionCreatorCommand>();
            container.RegisterType<IGeneratePagedListCommand, GeneratePagedListCommand>();

            container.RegisterType<IGenerateContainerCommand, GenerateContainerCommand>();
            container.RegisterType<IGenerateModuleCommand, GenerateModuleCommand>();
            container.RegisterType<IGenerateModelCommand, GenerateModelCommand>();
            container.RegisterType<IGenerateServiceCommand, GenerateServiceCommand>();
            container.RegisterType<IGenerateEditorCommand, GenerateEditorCommand>();
            container.RegisterType<IGenerateListCommand, GenerateListCommand>();
            container.RegisterType<IGenerateComponentCommand, GenerateComponentCommand>();
            container.RegisterType<IGenerateFeatureCommand, GenerateFeatureCommand>();

            container.RegisterType<Commands.Angular2.Contracts.IGenerateComponentCommand, Commands.Angular2.GenerateComponentCommand>();
            container.RegisterType<Commands.Angular2.Contracts.IGenerateFeatureCommand, Commands.Angular2.GenerateFeatureCommand>();
            container.RegisterType<Commands.Angular2.Contracts.IGenerateBootstrapCommand, Commands.Angular2.GenerateBootstrapCommand>();
            container.RegisterType<Commands.Angular2.Contracts.IGenerateModuleCommand, Commands.Angular2.GenerateModuleCommand>();
            container.RegisterType<Commands.Angular2.Contracts.IGenerateIndexCommand, Commands.Angular2.GenerateIndexCommand>();

            container.RegisterType<Commands.AspNetWebApi2.Contracts.IGenerateControllerCommand, Commands.AspNetWebApi2.GenerateControllerCommand>();
            container.RegisterType<Commands.AspNetWebApi2.Contracts.IGenerateDtosCommand, Commands.AspNetWebApi2.GenerateDtosCommand>();
            container.RegisterType<Commands.AspNetWebApi2.Contracts.IGenerateServiceCommand, Commands.AspNetWebApi2.GenerateServiceCommand>();

            container.RegisterType<IGenerateCommandCommand, GenerateCommandCommand>();

            container.RegisterType<IGenerateNugetCommand, GenerateNugetCommand>();

            container.RegisterType<IGenerateMicroserviceCommand, GenerateMicroserviceCommand>();

            container.RegisterType<INamespaceManager, NamespaceManager>();
            container.RegisterType<IMainManager, MainManager>();
            container.RegisterType<IMicroserviceTemplateProcessor, MicroserviceTemplateProcessor>();

            container.RegisterType<Leora.Commands.React.Contracts.IGenerateComponentCommand, Leora.Commands.React.GenerateComponentCommand>();

            return container;
        }
    }
}
