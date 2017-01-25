using CommandLine;

namespace Leora.Commands.CQRS.Core
{
    public class BaseOptions: Leora.Commands.Options.BaseOptions
    {
        public BaseOptions()
        {
            Directory = System.Environment.CurrentDirectory;
        }

        [Option("directory", Required = false, HelpText = "Directory")]
        public string Directory { get; set; }

        [Option("name", Required = false, HelpText = "Name")]
        public string Name { get; set; }

        [Option("entity", Required = false, HelpText = "Entity")]
        public string Entity { get; set; }

        [Option("namespace", Required = false, HelpText = "NameSpace")]
        public string NameSpace { get; set; }

        [Option("rootnamespace", Required = false, HelpText = "RootNameSpace")]
        public string RootNamespace { get; set; }
    }
}
