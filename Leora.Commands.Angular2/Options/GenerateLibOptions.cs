using CommandLine;

namespace Leora.Commands.Angular2.Options
{
    public class GenerateLibOptions: BaseOptions
    {
        [Option('c',"Component", Required=true, HelpText="Component")]
        public string ComponentName { get; set; }
    }
}
