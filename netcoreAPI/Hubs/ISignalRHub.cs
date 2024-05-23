using netcoreAPI.Structures;

namespace netcoreAPI.Hubs
{
    public interface ISignalRHub
    {
        public void SendAllMessageAsync(string message);

        public void SendAllDataAsync(string type,  dynamic data);  
    }
}
