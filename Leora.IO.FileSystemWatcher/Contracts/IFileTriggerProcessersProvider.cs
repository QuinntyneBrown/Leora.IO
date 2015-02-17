using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leora.IO.FileSystemWatcher.Contracts
{
    public interface IFileTriggerProcessersProvider
    {
        IList<IFileTriggeredProcesser> Get();
    }
}
