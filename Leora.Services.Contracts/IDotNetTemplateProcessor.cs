namespace Leora.Services.Contracts
{
    public interface IDotNetTemplateProcessor
    {
        string[] ProcessTemplate(string[] template, string name, string namespacename);
    }
}
