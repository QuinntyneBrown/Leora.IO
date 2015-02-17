using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leora.IO.Configuration;
using Leora.IO.ExtensionMethods;
using Microsoft.Practices.Unity;



namespace Leora.IO.TypeScript.AngularJS
{
    public static class DataService
    {
        public static string[] Get(string moduleName, string className)
        {
            var lines = new List<string>();

            foreach (var line in File.ReadAllLines(AppConfiguration.Config.TemplatesPath + @"\TypeScript\Angular\dataService.txt"))
            {
                var newline = line.Replace("{{ moduleNameCamelCase }}", moduleName.CamelCase());
                newline = newline.Replace("{{ moduleName }}", moduleName.FirstCharToUpper());
                newline = line.Replace("{{ classNameCamelCase }}", className.CamelCase());
                newline = newline.Replace("{{ className }}", className.FirstCharToUpper());
                lines.Add(newline);
            }

            return lines.ToArray();
        }
    }
}
