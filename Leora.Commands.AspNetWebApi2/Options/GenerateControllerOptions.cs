﻿using CommandLine;

namespace Leora.Commands.AspNetWebApi2.Options
{
    public class GenerateControllerOptions: BaseOptions
    {
        [Option("trace", Required = false, HelpText = "Trace")]
        public bool Trace { get; set; }

        [Option("cqrs", Required = false, HelpText = "CQRS")]
        public bool CQRS { get; set; }
    }
}
