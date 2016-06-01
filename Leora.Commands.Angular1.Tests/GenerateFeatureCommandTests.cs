using Leora.Commands.Angular1.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;

namespace Leora.Commands.Angular1.Tests
{
    [TestClass]
    public class GenerateFeatureCommandTests
    {
        private IGenerateFeatureCommand _generateFeatureCommand;

        [TestInitialize]
        public void SetUp()
        {
            _generateFeatureCommand = UnityConfiguration.GetContainer().Resolve<IGenerateFeatureCommand>();
        }

        [TestMethod]
        public void TestRunMethod()
        {
            Assert.AreEqual(_generateFeatureCommand.Run("customer", @"C:\test\",true), 1);
            Assert.AreEqual(_generateFeatureCommand.Run("customer", @"C:\test\",false), 1);
        }
    }
}
