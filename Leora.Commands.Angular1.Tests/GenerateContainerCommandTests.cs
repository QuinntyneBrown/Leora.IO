using Leora.Commands.Angular1.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;

namespace Leora.Commands.Angular1.Tests
{
    [TestClass]
    public class GenerateContainerCommandTests
    {
        private IGenerateContainerCommand _generateContainerCommand;

        [TestInitialize]
        public void SetUp()
        {
            _generateContainerCommand = UnityConfiguration.GetContainer().Resolve<IGenerateContainerCommand>();
        }

        [TestMethod]
        public void TestRunMethod()
        {
            Assert.AreEqual(_generateContainerCommand.Run("customer", @"C:\test\"), 1);
            Assert.AreEqual(_generateContainerCommand.Run("customer", @"C:\test\"), 1);
        }
    }
}
