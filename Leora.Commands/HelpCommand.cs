using static System.Console;

namespace Leora.Commands
{
    public class HelpCommand
    {
        public static int Run(string[] args) => Run(args, null);
        public static int Run(string[] args, string workspace)
        {
            PrintHelp();
            return 1;
        }

        public static void PrintHelp()
        {
            PrintVersionHeader();
            WriteLine(UsageText);
        }

        private const string UsageText = @"Usage: dotnet [common-options] [command] [arguments]
Arguments:
  [command]     The command to execute
  [arguments]   Arguments to pass to the command
Common Options (passed before the command):
  -v|--verbose  Enable verbose output
  --version     Display Chloe CLI Version Number
  --info        Display Chloe CLI Info
Common Commands:";

        public static void PrintVersionHeader()
        {
            WriteLine(Infrastructure.Constants.Version);
        }
    }
}
