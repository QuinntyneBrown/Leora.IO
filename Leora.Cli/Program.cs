﻿using Leora.Commands;
using Leora.Commands.Angular1.Contracts;
using Leora.Services.Contracts;
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
                ["generate-angular-action-creator"] = _container.Resolve<IGenerateActionCreatorCommand>().Run,
                ["generate-angular-component"] = _container.Resolve<IGenerateComponentCommand>().Run,
                ["generate-angular-paged-list"] = _container.Resolve<IGeneratePagedListCommand>().Run
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
