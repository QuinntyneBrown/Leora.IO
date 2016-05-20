using CommandLine;

namespace Leora.Commands.Angular2.Options
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
    }
}
