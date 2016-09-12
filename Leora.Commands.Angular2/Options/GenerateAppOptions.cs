using CommandLine;

namespace Leora.Commands.Angular2.Options
{
    public class GenerateAppOptions: BaseOptions
    {
        [Option("entity", Required = true, HelpText = "Entity")]
        public string Entity { get; set; }
    }
}
