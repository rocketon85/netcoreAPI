using Microsoft.EntityFrameworkCore;
using netcoreAPI.Context;
using netcoreAPI.Helper;
using netcoreAPI.Identity;

namespace netcoreAPI.Repositories
{
    public class UserRepository : BaseRepository, IRepository<User>
    {
        private EncryptorHelper _helperEncryptor;
        public UserRepository(AppDbContext dbContext, EncryptorHelper helperEncryptor) : base(dbContext)
        {
            _helperEncryptor = helperEncryptor;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await DbContext.Users.ToListAsync();
        }

        public async Task<User?> Get(string name, string password)
        {
            return await DbContext.Users.SingleOrDefaultAsync(p => p.Name.ToLower() == name.ToLower() && _helperEncryptor.DecryptString(p.Password) == password);
        }

        public async Task<User?> GetById(int id)
        {
            return await DbContext.Users.SingleOrDefaultAsync(p => p.Id == id);
        }

        public Task<User?> GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
