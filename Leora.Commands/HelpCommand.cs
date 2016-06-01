using System.Collections.Generic;
using System.IO;
using System.Reflection;
using static System.Console;
using static System.String;

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
            WriteLine(GetEmbeddedResource("UsageText.txt"));
        }
        
        public static void PrintVersionHeader()
        {
            WriteLine(Infrastructure.Constants.Version);
        }

        public static string GetEmbeddedResource(string resourceName)
        {
            List<string> lines = new List<string>();
            var assembly = Assembly.GetExecutingAssembly();
            using (System.IO.Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{resourceName}"))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
                return Join("\n", lines);
            }
        }
    }
}
