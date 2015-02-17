using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leora.IO.Configuration;
using Leora.IO.Data.Contracts;
using Microsoft.Practices.Unity;

namespace Leora.IO.TypeScript.AngularJS
{
    public static class Less
    {
        static Less()
        {
            templateRepository = UnityConfiguration.GetContainer().Resolve<ITemplateRepository>();
        }


        private static ITemplateRepository templateRepository { get; set; }

    }
}
