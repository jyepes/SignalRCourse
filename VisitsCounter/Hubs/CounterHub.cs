using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace VisitsCounter.Hubs
{
    public class CounterHub : Hub
    {
        private static int _visitantes = 0;

        public override async Task OnConnected()
        {
            Interlocked.Increment(ref _visitantes);
            await Clients.Others.Message("Nuevo conexión " + Context.ConnectionId + " Total Visistantes: " + _visitantes);
            await Clients.Caller.Message("Hola, Bienvenido...!!!");
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Interlocked.Decrement(ref _visitantes);
            return Clients.All.Message(Context.ConnectionId + " cerro la conexión." + " Total Visistantes: " + _visitantes);
        }

        public Task Broadcast(string message)
        {
            return Clients.All.Message(Context.ConnectionId + ">>" + message);
        }
    }
}