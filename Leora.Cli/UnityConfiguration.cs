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

            
            container.RegisterType<Commands.AspNetWebApi2.Contracts.IGenerateControllerCommand, Commands.AspNetWebApi2.GenerateControllerCommand>();
            container.RegisterType<Commands.AspNetWebApi2.Contracts.IGenerateDtosCommand, Commands.AspNetWebApi2.GenerateDtosCommand>();
            container.RegisterType<Commands.AspNetWebApi2.Contracts.IGenerateServiceCommand, Commands.AspNetWebApi2.GenerateServiceCommand>();
            container.RegisterType<Commands.AspNetWebApi2.Contracts.IGenerateModelCommand, Commands.AspNetWebApi2.GenerateModelCommand>();
            container.RegisterType<Commands.AspNetWebApi2.Contracts.IGenerateMigrationsConfigurationCommand, Commands.AspNetWebApi2.GenerateMigrationsConfigurationCommand>();
            container.RegisterType<Commands.AspNetWebApi2.Contracts.IGenerateUploadHandlersCommand, Commands.AspNetWebApi2.GenerateUploadHandlersCommand>();

            container.RegisterType<IGenerateCommandCommand, GenerateCommandCommand>();

            container.RegisterType<IGenerateNugetCommand, GenerateNugetCommand>();

            container.RegisterType<IGenerateMicroserviceCommand, GenerateMicroserviceCommand>();

            container.RegisterType<INamespaceManager, NamespaceManager>();
            container.RegisterType<IMainManager, MainManager>();
            container.RegisterType<IMicroserviceTemplateProcessor, MicroserviceTemplateProcessor>();

            container.RegisterType<Leora.Commands.React.Contracts.IGenerateComponentCommand, Leora.Commands.React.GenerateComponentCommand>();

            Angular2UnityConfiguration.RegisterTypes(container);

            return container;
        }
    }
}
