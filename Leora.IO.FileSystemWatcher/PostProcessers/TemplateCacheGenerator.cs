using Leora.IO.ExtensionMethods;
using Leora.IO.FileSystemWatcher.Contracts;
using Leora.IO.FileSystemWatcher.Enums;

namespace Leora.IO.FileSystemWatcher.PostProcessers
{
    public class TemplateCacheGenerator: Leora.IO.FileSystemWatcher.Path.BaseProcessor
    {
        public void Process(EventType eventType, string fullPath)
        {
            if (eventType == EventType.Change && fullPath.IsInsideAppFolder())
            {
                
            }
        }
    }
}
