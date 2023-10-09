using netcoreAPI.Dal;

namespace netcoreAPI.Repository
{
    public interface IRepository<T>
    {
        public abstract T GetById(int id);
        public abstract T GetByName(string name);
        public abstract IEnumerable<T> GetAll();
    }
}
