using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using netcoreAPI.Controllers;
using netcoreAPI.Dal;
using netcoreAPI.Domain;
using netcoreAPI.Identity;
using netcoreAPI.Models;
using netcoreAPI.Repository;

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
            await this.Clients.Others.SendCoreAsync("Send", new[] { message });
        }
    }
}
