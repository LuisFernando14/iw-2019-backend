using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorialMoya001.Models;

namespace TutorialMoya001.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> SaveAsync(User user);
        Task<List<User>> GetAll();
        Task<User> GetById(string partitionKey, string rowKey = "user");
        Task<User> Update(User user);
        Task<bool> Delete(string partitionKey, string rowKey = "user");
        Task<User> Login(Authentication data);
        int GetCount();
    }
}
