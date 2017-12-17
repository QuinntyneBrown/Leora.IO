using System;
using Leora.Services.Contracts;
using System.Xml.Linq;
using System.Linq;
using Leora.Models;
using System.Collections.Generic;
using static System.String;
using static System.IO.Directory;
using static System.IO.Path;

namespace Leora.Services
{
    public class LeoraJSONFileManager : ILeoraJSONFileManager
    {
        public LeoraJSONFile GetLeoraJSONFile(string path, int depth)
        {            
            var directories = GetDirectoryName(path).Split(DirectorySeparatorChar);

            if (directories.Length < depth)
                return null;

            var newDirectories = directories.Take(directories.Length - depth).ToArray();
            var computedPath = Join(DirectorySeparatorChar.ToString(), newDirectories);
            var jsonFile = GetFiles(computedPath, "leora.json");

            if (jsonFile.FirstOrDefault() != null)
            {                
                return Newtonsoft.Json.JsonConvert.DeserializeObject<LeoraJSONFile>(System.IO.File.ReadAllText(jsonFile.FirstOrDefault()));
            }
            else
            {
                depth = depth + 1;
                return GetLeoraJSONFile(path, depth);
            }           
        }
    }
}
