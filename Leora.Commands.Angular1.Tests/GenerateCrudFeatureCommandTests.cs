using Leora.Commands.Angular1.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;

namespace Leora.Commands.Angular1.Tests
{
    [TestClass]
    public class GenerateCrudFeatureCommandTests
    {
        private IGenerateCrudFeatureCommand _generateCrudFeatureCommand;

        [TestInitialize]
        public void SetUp()
        {
            _generateCrudFeatureCommand = UnityConfiguration.GetContainer().Resolve<IGenerateCrudFeatureCommand>();
        }

        [TestMethod]
        public void TestRunMethod()
        {
            Assert.AreEqual(_generateCrudFeatureCommand.Run("customer", @"C:\test\"), 1);
            Assert.AreEqual(_generateCrudFeatureCommand.Run("customer", @"C:\test\"), 1);
        }
    }
}
