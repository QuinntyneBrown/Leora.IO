using System.IO;
using System.Linq;
using Leora.IO.FileSystemWatcher.Contracts;
using Leora.IO.FileSystemWatcher.Enums;
using Leora.IO.ExtensionMethods;
using Leora.IO.Configuration;
using System.Collections.Generic;
using Microsoft.CSharp.RuntimeBinder;

namespace Leora.IO.FileSystemWatcher.Folders
{
    public class ModelPathProcesser : Leora.IO.FileSystemWatcher.Path.BaseProcessor
    {
        public override void Process(dynamic options)
        {
            string fullPath = options.fullPath;
            string entityNameSnakeCase = options.entityNameSnakeCase;
            EventType eventType = options.eventType;

            if ((eventType == EventType.Created || eventType == EventType.Change) && fullPath.IsInsideModelFolder())
            {
                System.Console.WriteLine("Model");
            }
        }

    }
}
