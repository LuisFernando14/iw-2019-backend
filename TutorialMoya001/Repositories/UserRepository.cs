using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TutorialMoya001.Entities;
using TutorialMoya001.Models;
using TutorialMoya001.Repositories.Interfaces;

namespace TutorialMoya001.Repositories
{
    public class UserRepository : IUsersRepository
    {
        private readonly string ConnectionString;
        private CloudTable cloudTable;

        public UserRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(this.ConnectionString);
            CloudTableClient cloudTableClient = storageAccount.CreateCloudTableClient();
            cloudTable = cloudTableClient.GetTableReference("Users");
        }

        public async Task<User> SaveAsync(User user)
        {
            var result = new Result<User>();
            TableResult tblRsult = null;
            try
            {
                UserEntity userEntity = new UserEntity(user.Email)
                {
                    Email = user.Email,
                    Name = user.Name,
                    LastName = user.LastName,
                    Password = user.Password
                };
                TableOperation insertOperation = TableOperation.Insert(userEntity);
                tblRsult = await cloudTable.ExecuteAsync(insertOperation);
            }
            catch (Exception e)
            {
                return null;
            }
            return tblRsult.Result as User; // o retornar el usuario
        }

        public async Task<bool> Delete(string partitionKey, string rowKey)
        {
            var result = new Result<User>();
            TableOperation retrieveOperation = TableOperation.Retrieve<UserEntity>(partitionKey, rowKey);
            TableResult retrievedResult = await cloudTable.ExecuteAsync(retrieveOperation);
            UserEntity deleteEntity = (UserEntity)retrievedResult.Result;
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                retrievedResult = await cloudTable.ExecuteAsync(deleteOperation);
                return true;
            }
            return false;
        }

        public async Task<List<User>> GetAll()
        {
            var result = new Result<List<User>>();
            var query = new TableQuery<UserEntity>();
            var list = new List<User>();
            var token = new TableContinuationToken();
            foreach(var entity in await cloudTable.ExecuteQuerySegmentedAsync(query, token))
            {
                list.Add(new User() {
                    Email = entity.Email,
                    Name = entity.Name,
                    LastName = entity.LastName,
                    Password = entity.Password
                });
            }
            return list;
        }

        public async Task<User> GetById(string partitionKey, string rowKey)
        {
            var user = new User();
            var result = new Result<User>();
            try
            {
                TableOperation retrieveOperation = TableOperation.Retrieve<UserEntity>(partitionKey, rowKey);
                TableResult retrievedResult = await cloudTable.ExecuteAsync(retrieveOperation);
                if (retrievedResult.Result != null)
                {
                    Console.WriteLine(((UserEntity)retrievedResult.Result));
                    user.Email = ((UserEntity)retrievedResult.Result).Email;
                    user.Name = ((UserEntity)retrievedResult.Result).Name;
                    user.LastName = ((UserEntity)retrievedResult.Result).LastName;
                    user.Password = ((UserEntity)retrievedResult.Result).Password;
                }
                else
                {
                    user = null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return user;
        }

        public async Task<User> Login(Authentication userAuth)
        {
            var user = new User();
            var partitionKey = userAuth.Email;
            var rowKey = "user";
            try
            {
                TableOperation retrieveOperation = TableOperation.Retrieve<UserEntity>(partitionKey, rowKey);
                TableResult retrievedResult = await cloudTable.ExecuteAsync(retrieveOperation);
                if (retrievedResult.Result != null)
                {
                    user.Email = ((UserEntity)retrievedResult.Result).Email;
                    user.Name = ((UserEntity)retrievedResult.Result).Name;
                    user.LastName = ((UserEntity)retrievedResult.Result).LastName;
                    user.Password = ((UserEntity)retrievedResult.Result).Password;
                    if(user.Password != userAuth.Password)
                    {
                        return null;
                    }
                }
                else
                {
                    user = null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return user;
        }

        public async Task<User> Update(User user)
        {
            var result = new Result<User>();
            TableOperation retrieveOperation = TableOperation.Retrieve<UserEntity>(user.Email, "user");
            TableResult retrievedResult = await cloudTable.ExecuteAsync(retrieveOperation);
            var us = new UserEntity();
            if (retrievedResult.Result != null)
            {
                us = retrievedResult.Result as UserEntity;
                us.Name = user.Name;
                us.LastName = user.LastName;
                us.Password = user.Password;
                var updateOperation = TableOperation.Replace(us);
                await cloudTable.ExecuteAsync(updateOperation);
                return retrievedResult.Result as User;
            }
            return null;
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }
    }
}
