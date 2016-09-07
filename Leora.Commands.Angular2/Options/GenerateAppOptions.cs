using CommandLine;

namespace Leora.Commands.Angular2.Options
{
    public class GenerateAppOptions
    {
        public GenerateAppOptions()
        {
            Directory = System.Environment.CurrentDirectory;
        }

        [Option("projectName", Required = false, HelpText = "Project Name")]
        public string ProjectName { get; set; }

        [Option("directory", Required = false, HelpText = "Directory")]
        public string Directory { get; set; }

        [Option("name", Required = false, HelpText = "Name")]
        public string Name { get; set; }
    }
}
