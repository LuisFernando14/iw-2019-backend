using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TutorialMoya001.Models;
using TutorialMoya001.Repositories.Interfaces;

namespace SmartHouseTesting
{
    class MoqTest
    {

        Mock<IUsersRepository> mock = new Mock<IUsersRepository>();
        User us1 = new User("lmartinez.bno@gmail.com", "Wilfred", "Martínez", "HardPass");

        [SetUp]
        public void Setup()
        {
            // mock.Setup(m => m.SaveAsync(us1).GetAwaiter().GetResult()).Returns(us1);
            // mock.Setup(m => m.GetById("lmartinez.bno@gmail.com", "user").GetAwaiter().GetResult()).Returns(us1);
            // mock.Setup(m => m.GetById("", "user").GetAwaiter().GetResult()).Returns(Task.FromResult<User>(us1).Result);
            mock.Setup(m => m.GetCount()).Returns(5);
        }

        [TearDown]
        public void TearDown()
        {
            // scope.Dispose();
        }

        [Test]
        public void Test1()
        {
            // Assert.Pass();
            Assert.AreEqual(5, mock.Object.GetCount());
            // Assert.AreEqual(us1, mock.Object.GetById("lmartinez.bno@gmail.com", "user"));
        }
    }
}
