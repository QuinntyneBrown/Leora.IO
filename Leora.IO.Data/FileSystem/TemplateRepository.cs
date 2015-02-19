using System;
using System.Linq;
using Leora.IO.Data.Contracts;
using Leora.IO.Models;
using System.IO;
using Leora.IO.Configuration;

namespace Leora.IO.Data.FileSystem
{
    public class TemplateRepository: ITemplateRepository
    {
        public Template GetByNameLanguageFramework(string name, string language, string framework)
        {
            return new Template()
            {
                Lines =
                    File.ReadAllLines(AppConfiguration.Config.TemplatesPath +
                                      string.Format(@"{0}\{1}\{2}.txt", language, framework, name))
            };
        }

        public Template GetByName(string name)
        {
            return new Template()
            {
                Lines =
                    File.ReadAllLines(AppConfiguration.Config.TemplatesPath +
                                      string.Format(@"{0}.txt", name))
            };
        }

        public IQueryable<Template> GetAll()
        {
            throw new NotImplementedException();
        }

        public Template GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Template entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Template entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Template entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
