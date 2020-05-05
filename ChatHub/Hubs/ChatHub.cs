using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace ChatHub.Hubs
{
    public class ChatHub : Microsoft.AspNet.SignalR.Hub
    {
        public void BroadcastMessage(Person person)
        {
            Clients.Group(person.Group).displayText(person.Group, person.Name, person.Message);
        }
        
        public Task Join(string groupName)
        {
            return Groups.Add(Context.ConnectionId, groupName);
        }

        public Task Leave(string groupName)
        {
            return Groups.Remove(Context.ConnectionId, groupName);
        }
    }
}