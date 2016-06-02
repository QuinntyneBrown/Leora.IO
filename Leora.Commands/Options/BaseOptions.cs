using CommandLine;
using static System.Environment;

namespace Leora.Commands.Options
{
    public class BaseOptions
    {
        public BaseOptions()
        {
            Directory = CurrentDirectory;
        }

        [Option("directory", Required = false, HelpText = "Directory")]
        public string Directory { get; set; }

        [Option("name", Required = false, HelpText = "Name")]
        public string Name { get; set; }

        [Option("name", Required = false, HelpText = "Namespace")]
        public string Namespace { get; set; }
    }
}
