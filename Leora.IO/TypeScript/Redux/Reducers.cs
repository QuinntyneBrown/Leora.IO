using Leora.IO.Configuration;
using Leora.IO.Data.Contracts;
using Microsoft.Practices.Unity;

namespace Leora.IO.TypeScript.Redux
{
    public static class Reducers
    {
        static Reducers()
        {
            templateRepository = UnityConfiguration.GetContainer().Resolve<ITemplateRepository>();
        }

        public static string[] Get(string entityName)
        {
            var lines = new List<string>();
            return lines.ToArray();
        }

        private static ITemplateRepository templateRepository;
    }
}
