﻿using Leora.Commands.Angular1.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;

namespace Leora.Commands.Angular1.Tests
{
    [TestClass]
    public class GenerateListCommandTests
    {
        private IGenerateListCommand _generateListCommand;

        [TestInitialize]
        public void SetUp()
        {
            _generateListCommand = UnityConfiguration.GetContainer().Resolve<IGenerateListCommand>();
        }

        [TestMethod]
        public void TestRunMethod()
        {
            Assert.AreEqual(_generateListCommand.Run("customer", @"C:\test\"), 1);
            Assert.AreEqual(_generateListCommand.Run("customer", @"C:\test\"), 1);
        }
    }
}
