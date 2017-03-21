﻿using Leora.Commands.AspNetWebApi2.Contracts;
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
            IGenerateApiModelCommand generateApiModelCommand
            )
        {
            _generateAddOrUpdateCommand = generateAddOrUpdateCommand;
            _generateGetCommand = generateGetCommand;
            _generateGeyByIdCommand = generateGetByIdCommand;
            _generateRemoveCommand = generateRemoveCommand;
            _generateControllerCommand = generateControllerCommand;
            _generateApiModelCommand = generateApiModelCommand;
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

            return 0;
        }

        private IGenerateAddOrUpdateCommand _generateAddOrUpdateCommand { get; set; }
        private IGenerateGetCommand _generateGetCommand { get; set; }
        private IGenerateGetByIdCommand _generateGeyByIdCommand { get; set; }
        private IGenerateRemoveCommand _generateRemoveCommand { get; set; }
        private IGenerateControllerCommand _generateControllerCommand { get; set; }
        private IGenerateApiModelCommand _generateApiModelCommand { get; set; }

    }
}