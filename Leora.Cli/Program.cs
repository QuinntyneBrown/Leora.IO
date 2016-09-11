using Leora.Commands;
using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular2.Contracts;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace Leora.Cli
{
    public class Program
    {
        private readonly IUnityContainer _container;
        private readonly Dictionary<string, Func<string[], int>> _commands;

        static void Main(string[] args)
        {            
            new Program().ProcessArgs(args);
        }

        public Program()
        {
            _container = UnityConfiguration.GetContainer();

            _commands = new Dictionary<string, Func<string[], int>>
            {
                ["help"] = HelpCommand.Run,
                ["ng-action-creator"] = _container.Resolve<IGenerateActionCreatorCommand>().Run,
                ["ng-component"] = _container.Resolve<Leora.Commands.Angular1.Contracts.IGenerateComponentCommand>().Run,
                ["ng-container"] = _container.Resolve<IGenerateContainerCommand>().Run,
                ["ng-editor"] = _container.Resolve<IGenerateEditorCommand>().Run,
                ["ng-feature"] = _container.Resolve<Leora.Commands.Angular1.Contracts.IGenerateFeatureCommand>().Run,
                ["ng-list"] = _container.Resolve<IGenerateListCommand>().Run,
                ["ng-model"] = _container.Resolve<Commands.Angular1.Contracts.IGenerateModelCommand>().Run,
                ["ng-module"] = _container.Resolve<Leora.Commands.Angular1.Contracts.IGenerateModuleCommand>().Run,
                ["ng-paged-list"] = _container.Resolve<IGeneratePagedListCommand>().Run,
                ["ng-service"] = _container.Resolve<Leora.Commands.Angular1.Contracts.IGenerateServiceCommand>().Run,

                ["ng2-bootstrap"] = _container.Resolve<IGenerateBootstrapCommand>().Run,
                ["ng2"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGenerateComponentCommand>().Run,
                ["ng2-feature"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGenerateFeatureCommand>().Run,
                ["ng2-module"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGenerateModuleCommand>().Run,
                ["ng2-index"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGenerateIndexCommand>().Run,
                ["ng2-package-json"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGeneratePackageJsonCommand>().Run,
                ["ng2-app-package-json"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGenerateAppPackageJsonCommand>().Run,
                ["ng2-readme"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGenerateReadMeCommand>().Run,
                ["ng2-webpack"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGenerateWebpackCommand>().Run,
                ["ng2-vendors"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGenerateVendorsCommand>().Run,
                ["ng2-polyfills"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGeneratePolyfillsCommand>().Run,
                ["ng2-routing"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGenerateRoutingCommand>().Run,
                ["ng2-app-module"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGenerateAppModuleCommand>().Run,
                ["ng2-reducer"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGenerateReducerCommand>().Run,
                ["ng2-constants"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGenerateActionConstantsCommand>().Run,
                ["ng2-actions"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGenerateActionsCommand>().Run,
                ["ng2-service"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGenerateServiceCommand>().Run,
                ["ng2-libs"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGenerateLibsCommand>().Run,
                ["ng2-model"] = _container.Resolve<Commands.Angular2.Contracts.IGenerateModelCommand>().Run,

                ["ng2-ts-config"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGenerateTsConfigJsonCommand>().Run,
                ["ng2-typings"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGenerateTypingsCommand>().Run,
                ["ng2-typedoc"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGenerateTypeDocJsonCommand>().Run,
                ["ng2-index-from-folder"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGenerateIndexFromFolderCommand>().Run,
                ["ng2-app-store"] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGenerateAppStoreCommand>().Run,
                ["."] = _container.Resolve<Leora.Commands.Angular2.Contracts.IGenerateIndexFromFolderCommand>().Run,

                ["dotnet-controller"] = _container.Resolve<Commands.AspNetWebApi2.Contracts.IGenerateControllerCommand>().Run,
                ["dotnet-service"] = _container.Resolve<Commands.AspNetWebApi2.Contracts.IGenerateServiceCommand>().Run,
                ["dotnet-dtos"] = _container.Resolve<Commands.AspNetWebApi2.Contracts.IGenerateDtosCommand>().Run,
                ["dotnet-model"] = _container.Resolve<Commands.AspNetWebApi2.Contracts.IGenerateModelCommand>().Run,
                ["dotnet-migration"] = _container.Resolve<Commands.AspNetWebApi2.Contracts.IGenerateMigrationsConfigurationCommand>().Run,
                ["dotnet-upload-handlers"] = _container.Resolve<Commands.AspNetWebApi2.Contracts.IGenerateUploadHandlersCommand>().Run,
                ["react-component"] = _container.Resolve<Leora.Commands.React.Contracts.IGenerateComponentCommand>().Run,

                ["command"] = _container.Resolve<Leora.Commands.Contracts.IGenerateCommandCommand>().Run,

                ["nuget"] = _container.Resolve<Leora.Commands.VisualStudio.Contracts.IGenerateNugetCommand>().Run,

                ["microservice"] = _container.Resolve<Leora.Commands.Contracts.IGenerateMicroserviceCommand>().Run,
            };
        }

        public int ProcessArgs(string[] args)
        {
            bool? verbose = null;
            var success = true;
            var command = string.Empty;
            var lastArg = 0;
            for (; lastArg < args.Length; lastArg++)
            {
                if (IsArg(args[lastArg], "version"))
                {
                    HelpCommand.PrintVersionHeader();
                    return 0;
                }
                else if (IsArg(args[lastArg], "h", "help"))
                {
                    HelpCommand.PrintHelp();
                    return 0;
                }
                else if (args[lastArg].StartsWith("-"))
                {
                    WriteLine($"Unknown option: {args[lastArg]}");
                    success = false;
                }
                else
                {
                    command = args[lastArg];
                    break;
                }                
            }

            var appArgs = (lastArg + 1) >= args.Length ? Enumerable.Empty<string>() : args.Skip(lastArg + 1).ToArray();

            int exitCode;
            Func<string[], int> builtIn;
            if (_commands.TryGetValue(command, out builtIn))
            {
                exitCode = builtIn(appArgs.ToArray());
            }
            else
            {
                exitCode = 0;
            }
            return exitCode;
        }

        private static bool IsArg(string candidate, string longName) => IsArg(candidate, shortName: null, longName: longName);

        private static bool IsArg(string candidate, string shortName, string longName)
        {
            return (shortName != null && candidate.Equals("-" + shortName)) || (longName != null && candidate.Equals("--" + longName));
        }
    }
}
