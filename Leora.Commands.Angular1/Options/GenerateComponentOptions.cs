namespace Leora.Commands.Angular1.Options
{
    public class GenerateComponentOptions
    {
        public GenerateComponentOptions()
        {
            Directory = System.Environment.CurrentDirectory;
        }

        public string Name { get; set; }

        public string Directory { get; set; }
    }
}
