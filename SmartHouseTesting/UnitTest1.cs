using NUnit.Framework;
using System;
using System.Text;
using System.Transactions;
using TutorialMoya001.Models;
using TutorialMoya001.Utils;

namespace Tests
{
    public class Tests
    {
        private TransactionScope scope;
        [SetUp]
        public void Setup()
        {
            scope = new TransactionScope();
        }

        [TearDown]
        public void TearDown()
        {
            scope.Dispose();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void ShouldReturnTrue()
        {
            var auth = new Authentication("lmartinez.bno@gmail.com", "hardPass");
            var res = auth.Login();
            Assert.True(res);
        }

        [Test]
        public void ShouldReturnString()
        {
            var guid = Guid.NewGuid();
            var util = new Util();
            var id = Convert.ToBase64String(guid.ToByteArray());
            var res = util.GenerateBase64StringByGUID(guid);
            Assert.AreEqual(id, res);
        }
    }
}