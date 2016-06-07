namespace Leora.Services.Contracts
{
    public interface IWebComponentsTemplateProcessor
    {
        string[] ProcessTemplate(string[] template, string name);
    }
}
