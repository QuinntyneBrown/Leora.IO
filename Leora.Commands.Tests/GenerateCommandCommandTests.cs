using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Leora.Commands.Contracts;

namespace Leora.Commands.Tests
{
    [TestClass]
    public class GenerateCommandCommandTests
    {
        private IGenerateCommandCommand _generateCommandCommand;

        [TestInitialize]
        public void SetUp()
        {
            _generateCommandCommand = UnityConfiguration.GetContainer().Resolve<IGenerateCommandCommand>();
        }

        [TestMethod]
        public void TestRunMethod()
        {
            Assert.AreEqual(_generateCommandCommand.Run("customer", @"C:\test\"), 1);
            Assert.AreEqual(_generateCommandCommand.Run("customer", @"C:\test\"), 1);
        }
    }
}
