using Entities;
using Microsoft.AspNet.SignalR;

namespace WebClient.Hubs
{
    public class BroadcastHub : Hub
    {
        private readonly Broadcast _broadcast;

        public BroadcastHub() : this(Broadcast.Instance)
        {
        }
        public BroadcastHub(Broadcast broadcast)
        {
            _broadcast = broadcast;
        }

        public void BroadcastNews(News news)
        {
            _broadcast.BroadcastNews(news, true);
        }

        public void OpenBroadcast()
        {
            _broadcast.OpenBroadcast();
        }
    }
}