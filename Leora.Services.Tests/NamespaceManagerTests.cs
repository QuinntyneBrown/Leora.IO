using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Leora.Services.Tests
{
    [TestClass]
    public class NamespaceManagerTests
    {
        NamespaceManager _namespaceManager;

        [TestInitialize]
        public void Setup()
        {
            _namespaceManager = new NamespaceManager();
        }

        [TestMethod]
        public void TestGetNamespaceMethod()
        {
            var ns = _namespaceManager.GetNamespace(@"C:\projects\e-commerce\Server\Attributes\ChloeHandleErrorAttribute.cs");
            Assert.AreEqual(ns, "Chloe.Server.Attributes");
        }
    }
}
