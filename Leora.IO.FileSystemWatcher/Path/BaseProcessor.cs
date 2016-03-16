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

        }

        public virtual void Process(EventType eventType, string fullPath, Dictionary<string, string> options)
        {

        }

        public virtual void Process(dynamic options)
        {

        }
    }
}
