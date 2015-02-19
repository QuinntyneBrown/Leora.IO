using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leora.IO.Configuration;
using Leora.IO.Data.Contracts;
using Leora.IO.ExtensionMethods;
using Microsoft.Practices.Unity;

namespace Leora.IO
{
    public static class LeoraTxt
    {
        static LeoraTxt()
        {
            templateRepository = UnityConfiguration.GetContainer().Resolve<ITemplateRepository>();
        }

        public static string[] Get()
        {
            return templateRepository.GetByName("leora").Lines;
        }

        private static ITemplateRepository templateRepository { get; set; }
    }
}
