using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using SignalrWebService.Models;

namespace SignalrWebService.Hubs
{
    public class PerformanceHub : Hub
    {
        private static int numberOfUsers = 0;

        public void SendPerformance(IList<PerformanceModel> performanceModels)
        {
            Clients.All.broadcastPerformance(performanceModels);
        }

        public void Heartbeat()
        {
            Clients.All.heartbeat();
        }

        public override Task OnConnected()
        {
            numberOfUsers++;
            Clients.All.numberOfUsers(numberOfUsers);

            return (base.OnConnected());
        }

        public override Task OnDisconnected(bool flag)
        {
            numberOfUsers--;
            Clients.All.numberOfUsers(numberOfUsers);
            return (base.OnDisconnected(flag));
        }
    }
}