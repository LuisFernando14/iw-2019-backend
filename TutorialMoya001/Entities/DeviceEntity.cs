using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace TutorialMoya001.Entities
{
    public class DeviceEntity : TableEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string IconName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public bool IsOn { get; set; }
        public string UserEmail { get; set; }

        public DeviceEntity()
        {

        }

        public DeviceEntity(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }
    }
}
