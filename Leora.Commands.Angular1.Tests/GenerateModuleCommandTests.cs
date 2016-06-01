using Leora.Commands.Angular1.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;

namespace Leora.Commands.Angular1.Tests
{
    [TestClass]
    public class GenerateModuleCommandTests
    {
        private IGenerateModuleCommand _generateModuleCommand;

        [TestInitialize]
        public void SetUp()
        {
            _generateModuleCommand = UnityConfiguration.GetContainer().Resolve<IGenerateModuleCommand>();
        }

        [TestMethod]
        public void TestRunMethod()
        {
            Assert.AreEqual(_generateModuleCommand.Run("customer", @"C:\test\",true), 1);
            Assert.AreEqual(_generateModuleCommand.Run("customer", @"C:\test\", false), 1);
        }
    }
}
