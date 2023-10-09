using netcoreAPI.Dal;
using netcoreAPI.Identity;

namespace netcoreAPI.Repository
{
    public class UserRepository : BaseRepository, IRepository<User>
    {
        public UserRepository(AppDbContext dbContext):base(dbContext) 
        {

        }

        public IEnumerable<User> GetAll()
        {
            return this.dbContext.Users.ToList();
        }

        public User Get(string name, string password)
        {
            return this.dbContext.Users.SingleOrDefault(p => p.Name.ToLower() == name.ToLower() && p.Password == password);
        }

        public User GetById(int id)
        {
            return this.dbContext.Users.SingleOrDefault(p => p.Id == id);
        }

        public User GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
