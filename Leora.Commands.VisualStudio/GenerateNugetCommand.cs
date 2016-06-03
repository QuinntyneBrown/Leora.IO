using Leora.Commands.VisualStudio.Contracts;
using Leora.Commands.VisualStudio.Options;
using Leora.Models;
using Leora.Services.Contracts;
using System;
using System.IO;
using System.Net;
using static System.IO.File;

namespace Leora.Commands.VisualStudio
{
    public class GenerateNugetCommand : BaseCommand<GenerateNugetOptions>, IGenerateNugetCommand
    {
        public GenerateNugetCommand(ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter, INamespaceManager namespaceManager)
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter,namespaceManager) {
        }
        public override int Run(GenerateNugetOptions options) => Run(options.Name, options.Directory);        

        public int Run(string name, string directory)
        {
            int exitCode = 1;

            var outputFilename = Path.GetFullPath($"{directory}\\nuget.exe");
            Console.WriteLine("Downloading latest version of NuGet.exe...");
            new WebClient().DownloadFile("https://nuget.org/nuget.exe", outputFilename);

            return exitCode;
        }

    }
}
