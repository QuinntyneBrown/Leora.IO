using CommandLine;

namespace Leora.Commands.Options
{
    public class GenerateMicroserviceOptions: BaseOptions
    {
        [Option("entity", Required = true, HelpText = "Entity")]
        public string Entity { get; set; }
    }
}
