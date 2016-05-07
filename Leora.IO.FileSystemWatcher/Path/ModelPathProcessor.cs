using System.IO;
using System.Linq;
using Leora.IO.FileSystemWatcher.Contracts;
using Leora.IO.FileSystemWatcher.Enums;
using Leora.IO.ExtensionMethods;
using Leora.IO.Configuration;
using System.Collections.Generic;
using Microsoft.CSharp.RuntimeBinder;
using System;

namespace Leora.IO.FileSystemWatcher.Folders
{
    public class ModelPathProcesser : Leora.IO.FileSystemWatcher.Path.BaseProcessor
    {
        public override void Process(dynamic options)
        {
            string fullPath = options.fullPath;
            string entityNameSnakeCase = options.entityNameSnakeCase;
            EventType eventType = options.eventType;

            if (((eventType == EventType.Created || eventType == EventType.Change) && fullPath.IsInsideModelFolder()) && System.IO.Path.GetExtension(fullPath) == ".cs")
            {
                //input.Split(System.IO.Path.DirectorySeparatorChar)[input.Split(System.IO.Path.DirectorySeparatorChar).Count() - 3] != "wwwroot")

                var serverFolderPath = GetServerFolder(fullPath);
                var hasBaseEntity = false;
                string entityName =  System.IO.Path.GetFileNameWithoutExtension(fullPath);

                foreach (var line in File.ReadAllLines(fullPath))
                {
                    if(line.Contains(":"))
                    {
                        hasBaseEntity = true;
                    }
                }

                if(hasBaseEntity == false)
                {
                    File.WriteAllLines(string.Format(serverFolderPath + @"\Dtos\{0}Dto.cs", entityName), CSharp.WebAPI.Dto.Get(entityName));
                    File.WriteAllLines(string.Format(serverFolderPath + @"\Dtos\{0}AddOrUpdateRequestDto.cs", entityName), CSharp.WebAPI.Dto.GetRequest(entityName));
                    File.WriteAllLines(string.Format(serverFolderPath + @"\Dtos\{0}AddOrUpdateResponseDto.cs", entityName), CSharp.WebAPI.Dto.GetResponse(entityName));
                    File.WriteAllLines(string.Format(serverFolderPath + @"\Services\{0}Service.cs", entityName), CSharp.WebAPI.Service.Get(entityName));
                    File.WriteAllLines(string.Format(serverFolderPath + @"\Services\Contracts\I{0}Service.cs", entityName), CSharp.WebAPI.Service.GetInterface(entityName));
                    File.WriteAllLines(string.Format(serverFolderPath + @"\Controllers\{0}Controller.cs", entityName), CSharp.WebAPI.Controller.Get(entityName));
                    File.WriteAllLines(string.Format(serverFolderPath + @"\Exceptions\{0}NotFound.cs", entityName), CSharp.WebAPI.Exception.Get(entityName));
                }
            }
        }

        public string GetServerFolder(string input)
        {
            var value = "";

            var parts = input.Split(System.IO.Path.DirectorySeparatorChar).Count() - 3;

            foreach(var part in input.Split(System.IO.Path.DirectorySeparatorChar)) {

                if (input.Split(System.IO.Path.DirectorySeparatorChar).Count() - value.Split(System.IO.Path.DirectorySeparatorChar).Count() > 2)
                {
                    if(value =="")
                    {
                        value = part;
                    }
                    else
                    {
                        value = string.Format(@"{0}\{1}", value, part);
                    }
                    
                }
            }

            return value;
        }

    }
}
