using Microsoft.AspNetCore.SignalR;
using netcoreAPI.Structures;

namespace netcoreAPI.Hubs
{
    public class SignalRHub : Hub, ISignalRHub
    {

        public SignalRHub()
        {

        }

        //define all methods that the hub listens to
        public async void SendAllMessageAsync(string message)
        {
            await Clients.All.SendCoreAsync("SendAllMessageAsync", new[] { message });
        }

        public async void SendAllDataAsync(string type, dynamic data)
        {
            await Clients.All.SendCoreAsync(type, data);
        }
    }
}
