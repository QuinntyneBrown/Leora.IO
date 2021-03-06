﻿using CommandLine;

namespace Leora.Commands.AspNetCore.Options
{
    public class BaseOptions
    {
        public BaseOptions()
        {
            Directory = System.Environment.CurrentDirectory;
        }

        [Option("directory", Required = false, HelpText = "Directory")]
        public string Directory { get; set; }

        [Option("name", Required = false, HelpText = "Name")]
        public string Name { get; set; }

        [Option("namespace", Required = false, HelpText = "NameSpace")]
        public string NameSpace { get; set; }

        [Option("rootnamespace", Required = false, HelpText = "RootNameSpace")]
        public string RootNamespace { get; set; }
    }
}
