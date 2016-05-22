using CommandLine;

namespace Leora.Commands.Angular1.Options
{
    public class GenerateServiceOptions: BaseOptions
    {
        [Option("crud", Required = false, HelpText = "Crud")]
        public bool Crud { get; set; }

        [Option("data", Required = false, HelpText = "Data")]
        public bool Data { get; set; }
    }
}
