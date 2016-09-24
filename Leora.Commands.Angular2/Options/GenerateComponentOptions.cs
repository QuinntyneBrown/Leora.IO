using CommandLine;

namespace Leora.Commands.Angular2.Options
{
    public class GenerateComponentOptions: BaseOptions
    {
        [Option("s", Required = false, HelpText = "Simple")]
        public bool Simple { get; set; }
    }
}
