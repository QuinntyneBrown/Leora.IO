using Leora.IO.FileSystemWatcher.Contracts;
using Leora.IO.FileSystemWatcher.Enums;
using Leora.IO.Paths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leora.IO.FileSystemWatcher.Path
{
    public class SolutionPathProcessor : IFileTriggeredProcesser
    {
        public void Process(EventType eventType, string fullPath)
        {
            if (eventType == EventType.Created && SolutionPath.IsInsideSolutionPath(fullPath))
            {
                
            }
        }
    }
}
