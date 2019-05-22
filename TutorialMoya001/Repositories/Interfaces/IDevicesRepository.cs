using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorialMoya001.Models;

namespace TutorialMoya001.Repositories.Interfaces
{
    public interface IDevicesRepository
    {
        Task<Device> Save(Device device);
        Task<List<Device>> GetAll(string partitionKey);
        Task<Device> GetById(string partitionKey, string rowKey);
        Task<Device> Update(Device device);
        Task<bool> Delete(string partitionKey, string rowKey);
    }
}
