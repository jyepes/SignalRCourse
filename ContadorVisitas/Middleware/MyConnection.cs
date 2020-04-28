using System.Threading.Tasks;
using System.Threading;
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
        protected override async Task OnConnected(IRequest request, string connectionId)
        {
            Interlocked.Increment(ref _connection);
            await Connection.Send(connectionId, "Welcome! " + connectionId);
            await Connection.Broadcast("New connection: " + connectionId + ". Current visits: " + _connection);

        }


        /// <summary>
        /// Este método se dispara cuando se reciben datos desde le cliente
        /// </summary>
        /// <param name="request"></param>
        /// <param name="connectionId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            var message = connectionId + ": " + data;
            return Connection.Broadcast(message);
        }

        /// <summary>
        /// Este método se dispara cuando un cliente se desconecta
        /// </summary>
        /// <param name="request"></param>
        /// <param name="connectionId"></param>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        {
            Interlocked.Decrement(ref _connection);
            return Connection.Broadcast(connectionId + " salió. Current visits: " + _connection);
        }
    }
}