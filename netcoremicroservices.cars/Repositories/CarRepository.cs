using Microsoft.EntityFrameworkCore;
using NetCoreMicroservices.Cars.Context;
using NetCoreMicroservices.Cars.Domains;
using NetCoreMicroservices.Commons.Repositories;

namespace NetCoreMicroservices.Cars.Repositories
{
    public interface ICarRepository : IBaseRepository<CarDomain>
    {

    }

    public class CarRepository : BaseRepository<CarDomain>, ICarRepository
    {
        AppDbContext GetContext() => (AppDbContext)base.Context;
        public CarRepository(DbContext context)
            : base(context)
        {

        }

        //public new async Task<IEnumerable<CarDomain>> FindAll()
        //{
        //    return await base.FindAll().Include(p => p.Fuel)
        //        .Include(p => p.Model)
        //        .Include(p => p.Brand)
        //        .Include(p => p.Fuel)
        //        .Select(p => p)
        //        .ToListAsync();
        //}

      
        //public async Task<CarDomain?> GetById(int id)
        //{
        //    return await GetContext().Cars.Include(p => p.Model).Include(p => p.Brand).Include(p => p.Fuel).SingleOrDefaultAsync(p => p.Id == id);
        //}

        //public async Task<CarDomain?> GetByName(string name)
        //{
        //    return await GetContext().Cars.SingleOrDefaultAsync(p => p.Name.ToLowerInvariant() == name.ToLowerInvariant());
        //}

        public override async Task<CarDomain?> CreateAsync(CarDomain model, CancellationToken cancellation = default(CancellationToken))
        {
            await base.CreateAsync(model);
            int result = await SaveChangeAsync(cancellation);
            var car = await FindByCondition(p => p.Id == model.Id).FirstOrDefaultAsync();
            //if (result == 1) azureFuncService.FuncNewCar(car);
            return model;
        }
    }
}
