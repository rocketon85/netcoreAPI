namespace netcoreAPI.Repositories
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        ICarRepository Car { get; }
        Task<int> SaveAsync();

        int Save();
    }
}
