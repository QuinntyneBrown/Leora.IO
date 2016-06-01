using Leora.Commands.Angular1.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;

namespace Leora.Commands.Angular1.Tests
{
    [TestClass]
    public class GenerateActionCreatorCommandTests
    {
        private IGenerateActionCreatorCommand _generateActionCreatorCommand;

        [TestInitialize]
        public void SetUp()
        {
            _generateActionCreatorCommand = UnityConfiguration.GetContainer().Resolve<IGenerateActionCreatorCommand>();
        }

        [TestMethod]
        public void TestRunMethod()
        {
            Assert.AreEqual(_generateActionCreatorCommand.Run("customer", @"C:\test\", true), 1);
            Assert.AreEqual(_generateActionCreatorCommand.Run("customer", @"C:\test\", false), 1);
        }
    }
}
