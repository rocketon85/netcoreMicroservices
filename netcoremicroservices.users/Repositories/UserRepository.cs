using Microsoft.EntityFrameworkCore;
using NetCoreMicroservices.Users.Context;
using NetCoreMicroservices.Users.Domains;
using NetCoreMicroservices.Commons.Repositories;

namespace NetCoreMicroservices.Users.Repositories
{
    public interface IUserRepository : IBaseRepository<UserDomain>
    {
        Task<string> Authenticate(UserDomain domain);
    }

    public class UserRepository : BaseRepository<UserDomain>, IUserRepository
    {
        AppDbContext GetContext() => (AppDbContext)base.Context;
        public UserRepository(DbContext context)
            : base(context)
        {

        }

     


        //public async Task<CarDomain?> GetById(int id)
        //{
        //    return await GetContext().Cars.Include(p => p.Model).Include(p => p.Brand).Include(p => p.Fuel).SingleOrDefaultAsync(p => p.Id == id);
        //}

        //public async Task<CarDomain?> GetByName(string name)
        //{
        //    return await GetContext().Cars.SingleOrDefaultAsync(p => p.Name.ToLowerInvariant() == name.ToLowerInvariant());
        //}

        public override async Task<UserDomain?> CreateAsync(UserDomain model, CancellationToken cancellation = default(CancellationToken))
        {
            await base.CreateAsync(model);
            int result = await SaveChangeAsync(cancellation);
            var car = await FindByCondition(p => p.Name == model.Name).FirstOrDefaultAsync();
            //if (result == 1) azureFuncService.FuncNewCar(car);
            return model;
        }

        public Task<string> Authenticate(UserDomain domain)
        {
            throw new NotImplementedException();
        }
    }
}
