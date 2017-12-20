namespace Leora.Services.Contracts
{
    public interface ITemplateProcessor
    {
        string[] ProcessTemplate(string[] template, string name, string namespacename = "");
    }
}
