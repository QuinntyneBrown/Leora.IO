using Leora.Commands.Angular1.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;

namespace Leora.Commands.Angular1.Tests
{
    [TestClass]
    public class GenerateServiceCommandTests
    {
        private IGenerateServiceCommand _generateServiceCommand;

        [TestInitialize]
        public void SetUp()
        {
            _generateServiceCommand = UnityConfiguration.GetContainer().Resolve<IGenerateServiceCommand>();
        }

        [TestMethod]
        public void TestRunMethod()
        {
            Assert.AreEqual(_generateServiceCommand.Run("customer",@"C:\",true,true), 1);
        }
    }
}
