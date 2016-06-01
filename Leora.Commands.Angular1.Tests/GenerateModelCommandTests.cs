using Leora.Commands.Angular1.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;

namespace Leora.Commands.Angular1.Tests
{
    [TestClass]
    public class GenerateModelCommandTests
    {
        private IGenerateModelCommand _generateModelCommand;

        [TestInitialize]
        public void SetUp()
        {
            _generateModelCommand = UnityConfiguration.GetContainer().Resolve<IGenerateModelCommand>();
        }

        [TestMethod]
        public void TestRunMethod()
        {
            Assert.AreEqual(_generateModelCommand.Run("customer", @"C:\test\"), 1);
            Assert.AreEqual(_generateModelCommand.Run("customer", @"C:\test\"), 1);
        }
    }
}
