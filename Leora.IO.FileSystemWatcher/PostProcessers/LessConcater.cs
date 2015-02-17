using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leora.IO.FileSystemWatcher.Contracts;
using Leora.IO.ExtensionMethods;
using Leora.IO.FileSystemWatcher.Enums;

namespace Leora.IO.FileSystemWatcher.PostProcessers
{
    public class LessConcater: IFileTriggeredProcesser
    {
        public void Process(EventType eventType, string fullPath)
        {

        }
    }
}
