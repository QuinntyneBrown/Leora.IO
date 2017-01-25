namespace Leora.Services.Contracts
{
    public interface IDotNetTemplateProcessor
    {
        string[] ProcessTemplate(string[] template, string name, string namespacename, string rootNamespace);

        string[] ProcessTemplate(string[] template, string name, string namespacename);

        string[] ProcessTemplate(string[] template, string entityName, string name, string namespacename, string rootNamespace);
    }
}
