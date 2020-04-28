using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ContadorVisitas.Middleware
{
    public class MyConnection : PersistentConnection
    {
        private static int _connection = 0;

        /// <summary>
        /// Este método se ejecuta cuando un cliente se conecta
        /// </summary>
        /// <param name="request"></param>
        /// <param name="connectionId">Identificador de la conexión</param>
        /// <returns></returns>
        protected override Task OnConnected(IRequest request, string connectionId)
        {
            return Connection.Send(connectionId, "Welcome!");
        }

        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            return Connection.Broadcast(data);
        }
    }
}