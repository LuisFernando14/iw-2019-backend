using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TutorialMoya001.Models;
using TutorialMoya001.Repositories.Interfaces;

namespace SmartHouseTesting
{
    class DBTest
    {
        internal class UserRepositoryFaker : IUsersRepository
        {
            List<User> list = new List<User>();
            ConcurrentBag<User> a = new ConcurrentBag<User>();

            public Task<bool> Delete(string partitionKey, string rowKey = "user")
            {
                throw new NotImplementedException();
            }

            public Task<List<User>> GetAll()
            {
                throw new NotImplementedException();
            }

            public Task<User> GetById(string partitionKey, string rowKey = "user")
            {
                throw new NotImplementedException();
            }

            public int GetCount()
            {
                throw new NotImplementedException();
            }

            public Task<User> Login(Authentication data)
            {
                throw new NotImplementedException();
            }

            public async Task<User> SaveAsync(User user)
            {
                a.Add(user);
                return user;
            }

            public Task<User> Update(User user)
            {
                throw new NotImplementedException();
            }
        }

        [SetUp]
        public void Setup()
        {
            // scope = new TransactionScope();
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
            var a = new User();
            a.Name = "Wilfrido";
            a.LastName = "Martinez";
            a.Email = "hola@hola.com";
            a.Password = "Pass123";
            var b = new UserRepositoryFaker();
            var ret = b.SaveAsync(a).GetAwaiter().GetResult();
            Assert.AreEqual(a, ret);
        }
    }
}
