using CommandLine;

namespace Leora.Commands.Angular1.Options
{
    public class GenerateFeatureOptions: BaseOptions
    {
        [Option("crud", Required = false, HelpText = "Crud")]
        public bool Crud { get; set; }

        [Option("simple", Required = false, HelpText = "Simple")]
        public bool Simple { get; set; }
    }
}
