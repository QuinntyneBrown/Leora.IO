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
            var ns = _namespaceManager.GetNamespace(@"C:\projects\e-commerce\Server\Attributes");
            Assert.AreEqual(ns.Namespace, "Chloe.Server.Attributes");
            Assert.AreEqual(ns.RootNamespace, "Chloe");
        }

        [TestMethod]
        public void TestGetNamespaceMethodShort()
        {
            var ns = _namespaceManager.GetNamespace(@"C:\projects\e-commerce");
            Assert.AreEqual(ns.Namespace, "Chloe");
            Assert.AreEqual(ns.RootNamespace, "Chloe");
        }

        [TestMethod]
        public void TestGetNamespaceMethodFirstFolder()
        {
            var ns = _namespaceManager.GetNamespace(@"C:\projects\simple\simple\src");
            Assert.AreEqual(ns.Namespace, "Simple.Src");
            Assert.AreEqual(ns.RootNamespace, "Simple");
        }
    }
}
