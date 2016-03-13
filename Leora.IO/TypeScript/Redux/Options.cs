using Leora.IO.ExtensionMethods;

namespace Leora.IO.TypeScript.Redux
{
    public class Options
    {
        public string EntityNameSnakeCase { get; set; }
        public string EntityNamePascalCase { get { return this.EntityNamePascalCase.SnakeCaseToPascalCase(); } }
        public string EntityNameCamelCase { get { return this.EntityNamePascalCase.CamelCase(); } }
        public bool? Crud { get; set; }        
    }
}
