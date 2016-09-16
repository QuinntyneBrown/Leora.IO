namespace Leora.Services.Contracts
{
    public interface ITemplateManager
    {
        string[] Get(Leora.Models.FileType fileType, string name, string framework = null);

        string[] Get(Leora.Models.FileType fileType, string name, string section, string entityName, string framework = null, string[] sufixList = null);

        string[] Get(string name, string framework = null);
    }
}
