using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorialMoya001.Entities
{
    public class UserEntity : TableEntity
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public UserEntity()
        {

        }

        public UserEntity(string email, string rowKey)
        {
            this.PartitionKey = email;
            this.RowKey = rowKey;
        }

        public UserEntity(string email) : this(email, "user")
        {
        }
    }
}
