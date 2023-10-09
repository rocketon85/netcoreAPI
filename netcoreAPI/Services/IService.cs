using netcoreAPI.Models;

namespace netcoreAPI.Services
{
    public interface IService<T>
    {
        public abstract Response Add(T entity);
        public abstract Response Update(T entity);
        public abstract Response Delete(T entity);
    }
}
