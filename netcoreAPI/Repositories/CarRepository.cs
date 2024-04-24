using Microsoft.EntityFrameworkCore;
using netcoreAPI.Context;
using netcoreAPI.Domains;
using netcoreAPI.Identity;
using netcoreAPI.Services;

namespace netcoreAPI.Repositories
{
    public class CarRepository : RepositoryBase<CarDomain>, ICarRepository
    {
        private readonly IAzureFuncService azureFuncService;

        public CarRepository(AppDbContext repositoryContext, IAzureFuncService azureFuncService) 
            : base(repositoryContext)
        {
            this.azureFuncService = azureFuncService;
        }

        public new async Task<IEnumerable<CarDomain>> FindAll()
        {
            return await base.FindAll().Include(p => p.Fuel)
                .Include(p => p.Model)
                .Include(p => p.Brand)
                .Include(p => p.Fuel)
                .Select(p => p)
                .ToListAsync();
        }

        //public IQueryable<CarDomain> GetAllQueryable()
        //{
        //    return DbContext.Cars.Include(p => p.Fuel)
        //        .Include(p => p.Model)
        //        .Include(p => p.Brand)
        //        .Include(p => p.Fuel)
        //        .Select(p => new CarDomain
        //        {
        //            Id = p.Id,
        //            Name = p.Name,
        //        });
        //}

        public async Task<CarDomain?> GetById(int id)
        {
            return await RepositoryContext.Cars.Include(p => p.Model).Include(p => p.Brand).Include(p => p.Fuel).SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<CarDomain?> GetByName(string name)
        {
            return await RepositoryContext.Cars.SingleOrDefaultAsync(p => p.Name.ToLowerInvariant() == name.ToLowerInvariant());
        }

        public override async Task<CarDomain?> CreateAsync(CarDomain model)
        {
            await base.CreateAsync(model);
            int result = await RepositoryContext.SaveChangesAsync();
            var car = await FindByCondition(p => p.Id == model.Id).FirstOrDefaultAsync();
            if (result == 1) azureFuncService.FuncNewCar(car);
            return model;
        }
    }
}
