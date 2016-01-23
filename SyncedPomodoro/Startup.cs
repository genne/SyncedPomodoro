using Microsoft.Owin;
using Owin;
using SyncedPomodoro;

[assembly: OwinStartup(typeof(Startup))]
namespace SyncedPomodoro
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}