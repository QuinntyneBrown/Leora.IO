using Leora.Commands.Angular1.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;

namespace Leora.Commands.Angular1.Tests
{
    [TestClass]
    public class GenerateEditorCommandTests
    {
        private IGenerateEditorCommand _generateEditorCommand;

        [TestInitialize]
        public void SetUp()
        {
            _generateEditorCommand = UnityConfiguration.GetContainer().Resolve<IGenerateEditorCommand>();
        }

        [TestMethod]
        public void TestRunMethod()
        {
            Assert.AreEqual(_generateEditorCommand.Run("customer", @"C:\test\"), 1);
            Assert.AreEqual(_generateEditorCommand.Run("customer", @"C:\test\"), 1);
        }
    }
}
