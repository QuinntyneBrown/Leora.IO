using Leora.IO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leora.IO.Data.Contracts
{
    public interface ITemplateRepository: IRepository<Template>
    {
        Template GetByName(string name);

        Template GetByNameLanguageFramework(string name, string language, string framework);
    }
}
