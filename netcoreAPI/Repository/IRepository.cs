namespace netcoreAPI.Repository
{
    public interface IRepository<T>
    {
        public abstract Task<T?> GetById(int id);
        public abstract Task<T?> GetByName(string name);
        public abstract Task<IEnumerable<T>> GetAll();
    }
}
