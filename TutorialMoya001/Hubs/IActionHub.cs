using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorialMoya001.Models;

namespace TutorialMoya001.Hubs
{
    public interface IActionHub
    {

        Task DeviceStatusChange(string deviceRowKey, string devicePartitionKey, bool status);
        Task DeviceIsOnChange(Device device);
    }
}
// hub = tubo abierto de ambos lados