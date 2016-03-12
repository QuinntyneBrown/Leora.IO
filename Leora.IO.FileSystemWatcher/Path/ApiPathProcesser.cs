using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leora.IO.FileSystemWatcher.Contracts;
using Leora.IO.FileSystemWatcher.Enums;

namespace Leora.IO.FileSystemWatcher.Path
{
    public class ApiPathProcesser: Leora.IO.FileSystemWatcher.Path.BaseProcessor
    {
        public void Process(Enums.EventType eventType, string fullPath)
        {
            if (eventType == EventType.Created)
            {
                
            }
        }

        public void Process(EventType eventType, string fullPath, Dictionary<string, string> options)
        {
            throw new NotImplementedException();
        }
    }
}
