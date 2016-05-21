namespace Leora.Services.Contracts
{
    public interface ITemplateManager
    {
        string[] Get(Leora.Models.FileType fileType, string name, string framework = null);
    }
}
