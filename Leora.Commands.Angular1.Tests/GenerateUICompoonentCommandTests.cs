using Leora.Commands.Angular1.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;

namespace Leora.Commands.Angular1.Tests
{
    [TestClass]
    public class GenerateUIComponentCommandTests
    {
        private IGenerateUIComponentCommand _generateUIComponentCommand;

        [TestInitialize]
        public void SetUp()
        {
            _generateUIComponentCommand = UnityConfiguration.GetContainer().Resolve<IGenerateUIComponentCommand>();
        }

        [TestMethod]
        public void TestRunMethod()
        {
            Assert.AreEqual(_generateUIComponentCommand.Run("customer", @"C:\test\"), 1);
            Assert.AreEqual(_generateUIComponentCommand.Run("customer", @"C:\test\"), 1);
        }
    }
}
