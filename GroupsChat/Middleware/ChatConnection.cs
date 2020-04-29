using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Web;

namespace GroupsChat.Middleware
{
    public class ChatConnection : PersistentConnection
    {
        protected override Task OnConnected(IRequest request, string connectionId)
        {
            System.Collections.Specialized.NameValueCollection queryString = HttpContext.Current.Request.QueryString;
            string nombreGrupo = queryString["groupChat"];
            return Groups.Add(connectionId, nombreGrupo);
        }

        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            string[] decoded = data.Split(':');
            string groupName = decoded[0];
            string userName = decoded[1];
            string message = decoded[2];

            return Groups.Send(groupName, userName + " dice: " + message);
        }
    }
}