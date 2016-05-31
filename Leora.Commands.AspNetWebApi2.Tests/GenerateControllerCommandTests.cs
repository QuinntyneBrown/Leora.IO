using Leora.Commands.AspNetWebApi2.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Leora.Services.Mocks;
using Leora.Services;

namespace Leora.Commands.AspNetWebApi2.Tests
{    
    [TestClass]
    public class GenerateControllerCommandTests
    {
        private IGenerateControllerCommand _generateControllerCommand;

        [TestInitialize]
        public void SetUp()
        {
            _generateControllerCommand = new GenerateControllerCommand(
                new MockFileWriter(),
                new MockTemplateManager(),
                new MockTemplateProcessor(),
                new NamingConventionConverter(),
                new MockNamespaceManager(),
                new MockProjectManager());
        }

        [TestMethod]
        public void TestRunMethod()
        {
        }
    }
}
