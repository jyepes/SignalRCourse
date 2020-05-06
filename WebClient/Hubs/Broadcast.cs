using Business;
using Entities;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;

namespace WebClient.Hubs
{
    public class Broadcast
    {
        //Objeto IHubConnectionContext nos va permitir llamar a los métodos definidos en el Cliente.
        private IHubConnectionContext<dynamic> Clients { get; set; }

        private Broadcast(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
        }

        private readonly static Lazy<Broadcast> _instance = new Lazy<Broadcast>(() => new Broadcast(GlobalHost.ConnectionManager.GetHubContext<BroadcastHub>().Clients));

        private readonly object _broadcastStateLock = new object();

        public static Broadcast Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public void BroadcastNews(News news, bool isNew)
        {
            string message = news.Minute + "' " + news.Message;
            Clients.All.displayText(message);

            if (isNew)
            {
                var instanceBL = new NewsBL();
                instanceBL.RegisterNews(news);
            }

        }

        public void OpenBroadcast()
        {
            lock (_broadcastStateLock)
            {
                var news = GetAllNews();
                foreach (var item in news)
                {
                    BroadcastNews(item, false);
                }
            }
        }

        private IEnumerable<News> GetAllNews()
        {
            var instanceBL = new NewsBL();
            return instanceBL.GetNews();
        }
    }
}