using Microsoft.Practices.Unity;

namespace Leora.Cli
{
    public class Angular2UnityConfiguration
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<Commands.Angular2.Contracts.IGenerateComponentCommand, Commands.Angular2.GenerateComponentCommand>();
            container.RegisterType<Commands.Angular2.Contracts.IGenerateFeatureCommand, Commands.Angular2.GenerateFeatureCommand>();
            container.RegisterType<Commands.Angular2.Contracts.IGenerateBootstrapCommand, Commands.Angular2.GenerateBootstrapCommand>();
            container.RegisterType<Commands.Angular2.Contracts.IGenerateModuleCommand, Commands.Angular2.GenerateModuleCommand>();
            container.RegisterType<Commands.Angular2.Contracts.IGenerateIndexCommand, Commands.Angular2.GenerateIndexCommand>();
            container.RegisterType<Commands.Angular2.Contracts.IGeneratePackageJsonCommand, Commands.Angular2.GeneratePackageJsonCommand>();
            container.RegisterType<Commands.Angular2.Contracts.IGenerateReadMeCommand, Commands.Angular2.GenerateReadMeCommand>();
            container.RegisterType<Commands.Angular2.Contracts.IGenerateIndexCommand, Commands.Angular2.GenerateIndexCommand>();
            container.RegisterType<Commands.Angular2.Contracts.IGenerateWebpackCommand, Commands.Angular2.GenerateWebpackCommand>();
            container.RegisterType<Commands.Angular2.Contracts.IGenerateAppPackageJsonCommand, Commands.Angular2.GenerateAppPackageJsonCommand>();
            container.RegisterType<Commands.Angular2.Contracts.IGenerateNormalizeCommand, Commands.Angular2.GenerateNormalizeCommand>();
            container.RegisterType<Commands.Angular2.Contracts.IGeneratePolyfillsCommand, Commands.Angular2.GeneratePolyfillsCommand>();
            container.RegisterType<Commands.Angular2.Contracts.IGenerateVendorsCommand, Commands.Angular2.GenerateVendorsCommand>();
            container.RegisterType<Commands.Angular2.Contracts.IGenerateRoutingCommand, Commands.Angular2.GenerateRoutingCommand>();
            container.RegisterType<Commands.Angular2.Contracts.IGenerateAppModuleCommand, Commands.Angular2.GenerateAppModuleCommand>();
        }
    }
}
