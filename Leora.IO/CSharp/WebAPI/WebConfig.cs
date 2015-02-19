﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leora.IO.Configuration;
using Leora.IO.Data.Contracts;
using Microsoft.Practices.Unity;

namespace Leora.IO.CSharp.WebAPI
{
    public static class WebConfig
    {
        static WebConfig()
        {
            templateRepository = UnityConfiguration.GetContainer().Resolve<ITemplateRepository>();
        }

        public static string[] Get()
        {
            return templateRepository.GetByName("webConfig").Lines;
        }

        private static ITemplateRepository templateRepository { get; set; }
    }
}
