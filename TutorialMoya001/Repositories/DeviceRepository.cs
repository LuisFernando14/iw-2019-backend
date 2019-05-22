using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorialMoya001.Models;
using TutorialMoya001.Repositories.Interfaces;
using TutorialMoya001.Entities;

namespace TutorialMoya001.Repositories
{
    public class DeviceRepository : IDevicesRepository
    {
        private readonly string ConnectionString;
        private CloudTable cloudTable;
        IDictionary<string, int> dict = new Dictionary<string, int>();
        public DeviceRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(this.ConnectionString);
            CloudTableClient cloudTableClient = storageAccount.CreateCloudTableClient();
            cloudTable = cloudTableClient.GetTableReference("Devices");
        }

        public async Task<bool> Delete(string partitionKey, string rowKey)
        {
            var result = new Result<Device>();
            TableOperation retrieveOperation = TableOperation.Retrieve<DeviceEntity>(partitionKey, rowKey);
            TableResult retrievedResult = await cloudTable.ExecuteAsync(retrieveOperation);
            DeviceEntity deleteEntity = (DeviceEntity)retrievedResult.Result;
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                var response = await cloudTable.ExecuteAsync(deleteOperation);

                return true;
            }
            return false;
        }

        public async Task<List<Device>> GetAll(string partitionKey)
        {
            var result = new Result<List<Device>>();
            var devices = new List<Device>();
            TableQuery<DeviceEntity> query = new TableQuery<DeviceEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));
            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<DeviceEntity> resultSegment = await cloudTable.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;

                foreach (DeviceEntity entity in resultSegment.Results)
                {
                    devices.Add(new Device(){
                        Id = entity.Id,
                        Name = entity.Name,
                        IconName = entity.IconName,
                        Description = entity.Description,
                        Status = entity.Status,
                        IsOn = entity.IsOn,
                        UserEmail = entity.UserEmail
                    });
                }
            } while (token != null);

            return devices;
        }

        public async Task<Device> GetById(string partitionKey, string rowKey)
        {
            var result = new Result<Device>();
            var device = new Device();
            try
            {
                TableOperation retrieveOperation = TableOperation.Retrieve<DeviceEntity>(partitionKey, rowKey);
                TableResult retrievedResult = await cloudTable.ExecuteAsync(retrieveOperation);
                if (retrievedResult.Result != null)
                {
                    device.Id = ((DeviceEntity)retrievedResult.Result).Id;
                    device.Name = ((DeviceEntity)retrievedResult.Result).Name;
                    device.IconName = ((DeviceEntity)retrievedResult.Result).IconName;
                    device.Description = ((DeviceEntity)retrievedResult.Result).Description;
                    device.Status = ((DeviceEntity)retrievedResult.Result).Status;
                    device.IsOn = ((DeviceEntity)retrievedResult.Result).IsOn;
                    device.UserEmail = ((DeviceEntity)retrievedResult.Result).UserEmail;

                    return device;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Device> Save(Device device)
        {
            var result = new Result<Device>();
            try
            {
                DeviceEntity deviceEntity = new DeviceEntity(device.UserEmail, device.Id)
                {
                    Id = device.Id,
                    Name = device.Name,
                    IconName = device.IconName,
                    Description = device.Description,
                    Status = device.Status,
                    IsOn = device.IsOn,
                    UserEmail = device.UserEmail
                };
                TableOperation insertOperation = TableOperation.Insert(deviceEntity);
                await cloudTable.ExecuteAsync(insertOperation);
            }
            catch (Exception e)
            {
                return null;
            }
            return device;
        }

        public async Task<Device> Update(Device device)
        {
            var result = new Result<Device>();
            TableOperation retrieveOperation = TableOperation.Retrieve<DeviceEntity>(device.UserEmail, device.Id);
            TableResult retrievedResult = await cloudTable.ExecuteAsync(retrieveOperation);
            var dev = new DeviceEntity();
            if (retrievedResult.Result != null)
            {
                dev = retrievedResult.Result as DeviceEntity;
                dev.Id = device.Id;
                dev.Name = device.Name;
                dev.IconName = device.IconName;
                dev.Description = device.Description;
                dev.Status = device.Status;
                dev.IsOn = device.IsOn;
                dev.UserEmail = device.UserEmail;
                var updateOperation = TableOperation.Replace(dev);
                await cloudTable.ExecuteAsync(updateOperation);
                return device;
            }
            return null;
        }
    }
}
