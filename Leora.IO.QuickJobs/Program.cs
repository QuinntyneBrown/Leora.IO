using Leora.Commands.Angular2;
using Leora.Commands.Angular2.Contracts;
using Leora.Services;
using Leora.Services.Contracts;
using Microsoft.Practices.Unity;
using System;

namespace Leora.IO.QuickJobs
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = UnityConfiguration.GetContainer(); ;
            IGenerateReadMeCommand generateReadMeCommand = container.Resolve<IGenerateReadMeCommand>();
            IGeneratePackageJsonCommand generatePackageJsonCommand = container.Resolve<IGeneratePackageJsonCommand>();

            foreach (var directory in System.IO.Directory.GetDirectories(@""))
            {
                generateReadMeCommand.Run(System.IO.Path.GetFileName(directory), directory);
                //generatePackageJsonCommand.Run(System.IO.Path.GetFileName(directory), directory);            
            }
            Console.Write("Done!");
            Console.ReadLine();
        }
    }

    public class UnityConfiguration
    {
        public static IUnityContainer GetContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<INamingConventionConverter, NamingConventionConverter>();
            container.RegisterType<ITemplateManager, TemplateManager>();
            container.RegisterType<ITemplateProcessor, TemplateProcessor>();
            container.RegisterType<IDotNetTemplateProcessor, DotNetTemplateProcessor>();
            container.RegisterType<IProjectManager, ProjectManager>();
            container.RegisterType<IFileWriter, FileWriter>();

            container.RegisterType<IGenerateReadMeCommand, GenerateReadMeCommand>();
            container.RegisterType<IGeneratePackageJsonCommand, GeneratePackageJsonCommand>();
            return container;
        }

    }
}
