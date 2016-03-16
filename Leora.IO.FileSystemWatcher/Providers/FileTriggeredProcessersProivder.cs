using System.Collections.Generic;
using Leora.IO.FileSystemWatcher.Contracts;
using Leora.IO.FileSystemWatcher.PostProcessers;
using Leora.IO.FileSystemWatcher.Folders;

namespace Leora.IO.FileSystemWatcher.Providers
{
    public class FileTriggeredProcessersProivder: IFileTriggerProcessersProvider
    {
        public IList<IFileTriggeredProcesser> Get()
        {
            var processers = new List<IFileTriggeredProcesser>();
            //processers.Add(new ApiPathProcesser());
            processers.Add(new AppPathProcesser());
            processers.Add(new ModelPathProcesser());
            //processers.Add(new ComponentsPathProcessor());
            //processers.Add(new ModulePathProcessor());
            //processers.Add(new ProjectFileProcessor());
            //processers.Add(new ServicesPathProcessor());
            //processers.Add(new Gulp());
            //processers.Add(new JSConcater());
            //processers.Add(new JasmineSpecConcater());
            //processers.Add(new LessConcater());
            //processers.Add(new TemplateCacheGenerator());
            //processers.Add(new JSAppStructureGenerator());
            //processers.Add(new TSSpecFileGenerator());
            return processers;
        }
    }
}
