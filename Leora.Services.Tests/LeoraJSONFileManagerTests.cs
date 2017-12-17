using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Leora.Services.Tests
{
    [TestClass]
    public class LeoraJSONFileManagerTests
    {
        [TestMethod]
        public void Get()
        {
            var value = _leoraJSONFileManager.GetLeoraJSONFile(@"C:\Projects\e-commerce-app\ClientApp\app", -1);
            Assert.IsFalse(value.UseMessaging);
        }

        [TestInitialize]
        public void Setup()
        {
            _leoraJSONFileManager = new LeoraJSONFileManager();
        }

        protected LeoraJSONFileManager _leoraJSONFileManager;
    }
}
