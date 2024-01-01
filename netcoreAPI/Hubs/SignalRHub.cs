using Microsoft.AspNetCore.SignalR;

namespace netcoreAPI.Hubs
{
    public class SignalRHub : Hub, ISignalRHub
    {

        public SignalRHub()
        {

        }

        //define all methods that the hub listens to
        public async Task SendAll(string message)
        {
            await Clients.All.SendCoreAsync("Send", new[] { message });
        }
    }
}
