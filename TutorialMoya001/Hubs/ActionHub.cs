// using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// using Microsoft.AspNet.SignalR.Hubs;

namespace TutorialMoya001.Hubs
{
    public class ActionHub : Hub<IActionHub>
    {
        public String HubName { get; private set; }
        private readonly ILogger logger;

        public ActionHub(ILogger<ActionHub> logger)
        {
            this.logger = logger;
        }

        public async Task JoinDashboardGroup(string partitionKey)
        {
            this.logger.LogInformation($"Client {Context.ConnectionId} is viewing {partitionKey}");
            await Groups.AddToGroupAsync(Context.ConnectionId, partitionKey);
        }
        public async Task LeaveDashboardGroup(string partitionKey)
        {
            this.logger.LogInformation($"Client {Context.ConnectionId} is no longer viewing {partitionKey}");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, partitionKey);

        }
    }
}


// 