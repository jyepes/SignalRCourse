using Microsoft.AspNet.SignalR;

namespace Broadcast.Hubs
{
    public class BroadcastHub : Hub
    {
        public void BroadcastMessage(string message)
        {
            Clients.All.displayText(message);
        }
    }
}