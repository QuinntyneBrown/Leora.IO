using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leora.IO.Configuration
{
    public static class UnityConfiguration
    {
        public static UnityContainer GetContainer()
        {
            return Container;
        }

        public static UnityContainer Container {get; set;}
    }
}
