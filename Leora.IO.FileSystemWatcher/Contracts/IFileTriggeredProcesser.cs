using Leora.IO.FileSystemWatcher.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leora.IO.FileSystemWatcher.Contracts
{
    public interface IFileTriggeredProcesser
    {
        void Process(EventType eventType, string fullPath);
        void Process(EventType eventType, string fullPath, Dictionary<string, string> options);
        void Process(dynamic options);
            
    }
}
