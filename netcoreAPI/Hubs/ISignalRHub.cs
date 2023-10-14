using netcoreAPI.Domain;
using netcoreAPI.Identity;
using netcoreAPI.Models;

namespace netcoreAPI.Hubs
{
    public interface ISignalRHub
    {
        public Task SendAll(string message);
    }
}
