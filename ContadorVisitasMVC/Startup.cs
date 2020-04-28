using System;
using System.Threading.Tasks;
using ContadorVisitasMVC.Middelware;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ContadorVisitasMVC.Startup))]

namespace ContadorVisitasMVC
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            var config = new ConnectionConfiguration();
            //config.EnableJSONP = true; //habilita conexiones desde otros dominios
            app.MapSignalR<MyConnection>("/realtime", config);
        }
    }
}
