using System;
using Leora.Models;
using Leora.Services.Contracts;

namespace Leora.Services
{
    public class NamingConventionConverter : INamingConventionConverter
    {
        public string Convert(NamingConvention to, string value)
        {
            return value;
        }

        public string Convert(NamingConvention from, NamingConvention to, string value)
        {
            return value;
        }
    }
}
