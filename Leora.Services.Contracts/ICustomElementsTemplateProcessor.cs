namespace Leora.Services.Contracts
{
    public interface ICustomElementsTemplateProcessor
    {
        string[] ProcessTemplate(string[] template, string name, string prefix);
    }
}
