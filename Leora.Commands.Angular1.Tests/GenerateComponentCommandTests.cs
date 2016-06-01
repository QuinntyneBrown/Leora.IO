using Leora.Commands.Angular1.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;

namespace Leora.Commands.Angular1.Tests
{
    [TestClass]
    public class GenerateComponentCommandTests
    {
        private IGenerateComponentCommand _generateComponentCommand;

        [TestInitialize]
        public void SetUp()
        {
            _generateComponentCommand = UnityConfiguration.GetContainer().Resolve<IGenerateComponentCommand>();
        }

        [TestMethod]
        public void TestRunMethod()
        {
            Assert.AreEqual(_generateComponentCommand.Run("customer", @"C:\test\"), 1);
            Assert.AreEqual(_generateComponentCommand.Run("customer", @"C:\test\"), 1);
        }
    }
}
