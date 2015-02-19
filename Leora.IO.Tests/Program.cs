using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leora.IO.ExtensionMethods;

namespace Leora.IO.Tests
{
    class Program
    {
        static void Main(string[] args)
        {

            var fullPath = @"C:\Projects\ngBlueprint\ngBlueprint";

            var projectDirectory = fullPath.GetProjectDirectory();

            Console.ReadLine();
        }
    }
}
