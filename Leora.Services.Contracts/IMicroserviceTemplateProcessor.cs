namespace Leora.Services.Contracts
{
    public interface IMicroserviceTemplateProcessor
    {
        string[] ProcessTemplate(string[] template, string name, string entity);
    }
}
