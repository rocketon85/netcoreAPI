namespace netcoreAPI.Hubs
{
    public interface ISignalRHub
    {
        public Task SendAll(string message);
    }
}
