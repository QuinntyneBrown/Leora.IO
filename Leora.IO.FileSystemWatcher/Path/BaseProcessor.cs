using Leora.IO.FileSystemWatcher.Contracts;
using System;
using System.Collections.Generic;
using Leora.IO.FileSystemWatcher.Enums;

namespace Leora.IO.FileSystemWatcher.Path
{
    public class BaseProcessor : IFileTriggeredProcesser
    {
        public virtual void Process(EventType eventType, string fullPath)
        {
            throw new NotImplementedException();
        }

        public virtual void Process(EventType eventType, string fullPath, Dictionary<string, string> options)
        {
            throw new NotImplementedException();
        }

        public virtual void Process(dynamic options)
        {
            throw new NotImplementedException(); 
        }
    }
}
