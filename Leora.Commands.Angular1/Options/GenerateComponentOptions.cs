using CommandLine;

namespace Leora.Commands.Angular1.Options
{
    public class GenerateComponentOptions: BaseOptions
    {
        [Option("framework", Required = false, HelpText = "Framework")]
        public bool Framework { get; set; }
    }
}
