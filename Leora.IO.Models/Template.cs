using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leora.IO.Models
{
    public class Template
    {
        public Template()
        {
            
        }

        public int Id { get; set; }

        public string[] Lines { get; set; }
    }
}
