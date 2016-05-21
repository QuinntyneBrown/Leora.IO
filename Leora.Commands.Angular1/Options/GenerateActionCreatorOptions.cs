using CommandLine;

namespace Leora.Commands.Angular1.Options
{
    public class GenerateActionCreatorOptions: BaseOptions
    {
        [Option("crud", Required = false, HelpText = "Crud")]
        public bool Crud { get; set; }
    }
}
