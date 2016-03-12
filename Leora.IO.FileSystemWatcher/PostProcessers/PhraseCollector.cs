using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leora.IO.FileSystemWatcher.Contracts;
using Leora.IO.FileSystemWatcher.Enums;

namespace Leora.IO.FileSystemWatcher.PostProcessers
{
    public class PhraseCollector : Leora.IO.FileSystemWatcher.Path.BaseProcessor
    {
        public void Process(EventType eventType, string fullPath)
        {
            if (eventType == EventType.Change)
            {
                // 1. .ts
                // 2. exclude .d.ts
                // 3. exclude .module.ts
                // 4. exclude .spec.ts
                // 5. Not inside enum folder
                // 6. If spec file doesn't exist then then add a spec file.
            }

            if (eventType == EventType.Created)
            {
                // 1. .ts
                // 2. exclude .d.ts
                // 3. exclude .module.ts
                // 4. exclude .spec.ts
                // 5. Not inside enum folder
                // 6. If spec file doesn't exist then then add a spec file.
            }
        }
    }
}
