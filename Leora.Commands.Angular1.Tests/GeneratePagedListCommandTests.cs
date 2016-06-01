using Leora.Commands.Angular1.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;

namespace Leora.Commands.Angular1.Tests
{
    [TestClass]
    public class GeneratePagedListCommandTests
    {
        private IGeneratePagedListCommand _generatePagedListCommand;

        [TestInitialize]
        public void SetUp()
        {
            _generatePagedListCommand = UnityConfiguration.GetContainer().Resolve<IGeneratePagedListCommand>();
        }

        [TestMethod]
        public void TestRunMethod()
        {
            Assert.AreEqual(_generatePagedListCommand.Run("customer", @"C:\test\"), 1);
            Assert.AreEqual(_generatePagedListCommand.Run("customer", @"C:\test\"), 1);
        }
    }
}
