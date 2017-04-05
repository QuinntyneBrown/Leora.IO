using Leora.Services;
using Leora.Services.Contracts;
using Microsoft.Practices.Unity;
using System;

namespace Leora.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var projectManager = GetContainer().Resolve<IProjectManager>();

            var result = projectManager.GetRelativePathAndProjFile(@"C:\projects\PhotoGalleryService\src\PhotoGalleryService\Features\Search\SearchConfiguration.cs");

            Console.ReadLine();

        }

        static IUnityContainer GetContainer() {
            var container = new UnityContainer();

            container.RegisterType<IProjectManager, ProjectManager>();

            return container;
        }
    }
}
