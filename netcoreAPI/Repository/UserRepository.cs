using Microsoft.EntityFrameworkCore;
using netcoreAPI.Dal;
using netcoreAPI.Helper;
using netcoreAPI.Identity;

namespace netcoreAPI.Repository
{
    public class UserRepository : BaseRepository, IRepository<User>
    {
        private EncryptorHelper helperEncryptor;
        public UserRepository(AppDbContext dbContext, EncryptorHelper helperEncryptor) :base(dbContext) 
        {
            this.helperEncryptor = helperEncryptor;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await this.dbContext.Users.ToListAsync();
        }

        public async Task<User?> Get(string name, string password)
        {
            return await this.dbContext.Users.SingleOrDefaultAsync(p => p.Name.ToLower() == name.ToLower() && this.helperEncryptor.DecryptString(p.Password) ==  password);
        }

        public async Task<User?> GetById(int id)
        {
            return await this.dbContext.Users.SingleOrDefaultAsync(p => p.Id == id);
        }

        public Task<User?> GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
