using Leora.Services.Contracts;
using Microsoft.Practices.Unity;
using static System.Console;
namespace Leora.Cli
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            var program = UnityConfiguration.GetContainer().Resolve<Program>();
            program.ProcessArgs(args);
        }

        public Program()
        {
            
        }

        public void ProcessArgs(string[] args)
        {

        }
    }
}
