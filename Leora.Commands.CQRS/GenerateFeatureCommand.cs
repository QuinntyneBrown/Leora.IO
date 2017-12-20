using Leora.Commands.AspNetWebApi2.Contracts;
using Leora.Commands.Contracts;
using Leora.Commands.CQRS.Core;
using Leora.Models;
using Leora.Models;
using Leora.Services.Contracts;
using static CommandLine.Parser;
using System.Linq;
using System;

namespace Leora.Commands.CQRS
{
    public class GenerateFeatureOptions: BaseOptions { }

    public interface IGenerateFeatureCommand : ICommand { }

    public class GenerateFeatureCommand : IGenerateFeatureCommand
    {
        public GenerateFeatureCommand(
            IGenerateAddOrUpdateCommand generateAddOrUpdateCommand,
            IGenerateGetCommand generateGetCommand,
            IGenerateGetByIdCommand generateGetByIdCommand,
            IGenerateRemoveCommand generateRemoveCommand,
            IGenerateControllerCommand generateControllerCommand,
            IGenerateApiModelCommand generateApiModelCommand,
            IGenerateRemovedMessageCommand generateRemovedMessageCommand,
            IGenerateAddedOrUpdatedMessageCommand generateAddedOrUpdatedMessageCommand,
            IGenerateCacheKeyFactoryCommand generateCacheKeyFactoryCommand,
            IGenerateEventBusMessagesCommand generateEventBusMessagesCommand,
            IGenerateEventBusMessageHandlerCommand generateEventBusMessageHandlerCommand,
            ILeoraJSONFileManager leoraJSONFileManager
            )
        {
            _generateAddOrUpdateCommand = generateAddOrUpdateCommand;
            _generateGetCommand = generateGetCommand;
            _generateGeyByIdCommand = generateGetByIdCommand;
            _generateRemoveCommand = generateRemoveCommand;
            _generateControllerCommand = generateControllerCommand;
            _generateApiModelCommand = generateApiModelCommand;
            _generateAddedOrUpdatedMessageCommand = generateAddedOrUpdatedMessageCommand;
            _generateRemovedMessageCommand = generateRemovedMessageCommand;
            _generateCacheKeyFactoryCommand = generateCacheKeyFactoryCommand;
            _generateEventBusMessageHandlerCommand = generateEventBusMessageHandlerCommand;
            _generateEventBusMessagesCommand = generateEventBusMessagesCommand;
            _leoraJSONFileManager = leoraJSONFileManager;
        }

        public int Run(string[] args)
        {
            var argsList = args.ToList();
            argsList.Add("--cqrs");

            _generateAddOrUpdateCommand.Run(argsList.ToArray());
            _generateGetCommand.Run(argsList.ToArray());
            _generateGeyByIdCommand.Run(argsList.ToArray());
            _generateRemoveCommand.Run(argsList.ToArray());
            
            argsList.Add("--name");
            argsList.Add(argsList.ElementAt(1));
     
            _generateApiModelCommand.Run(argsList.ToArray());
            _generateControllerCommand.Run(argsList.ToArray());

            if (_leoraJSONFileManager.GetLeoraJSONFile(System.Environment.CurrentDirectory, -1).UseMessaging) {
                _generateCacheKeyFactoryCommand.Run(argsList.ToArray());
                _generateEventBusMessageHandlerCommand.Run(argsList.ToArray());
                _generateEventBusMessagesCommand.Run(argsList.ToArray());
                _generateAddedOrUpdatedMessageCommand.Run(argsList.ToArray());
                _generateRemovedMessageCommand.Run(argsList.ToArray());
            }

            return 0;
        }

        private IGenerateAddOrUpdateCommand _generateAddOrUpdateCommand { get; set; }
        private IGenerateGetCommand _generateGetCommand { get; set; }
        private IGenerateGetByIdCommand _generateGeyByIdCommand { get; set; }
        private IGenerateRemoveCommand _generateRemoveCommand { get; set; }
        private IGenerateControllerCommand _generateControllerCommand { get; set; }
        private IGenerateApiModelCommand _generateApiModelCommand { get; set; }
        private IGenerateAddedOrUpdatedMessageCommand _generateAddedOrUpdatedMessageCommand { get; set; }
        private IGenerateCacheKeyFactoryCommand _generateCacheKeyFactoryCommand { get; set; }
        private IGenerateEventBusMessagesCommand _generateEventBusMessagesCommand { get; set; }
        private IGenerateRemovedMessageCommand _generateRemovedMessageCommand { get; set; }

        private IGenerateEventBusMessageHandlerCommand _generateEventBusMessageHandlerCommand { get; set; }
        protected ILeoraJSONFileManager _leoraJSONFileManager;
    }
}
