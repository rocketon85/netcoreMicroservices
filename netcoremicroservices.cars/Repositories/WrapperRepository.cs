using Microsoft.EntityFrameworkCore;
using NetCoreMicroservices.Cars.Context;

namespace NetCoreMicroservices.Cars.Repositories
{
    public class WrapperRepository : IWrapperRepository
    {
        private readonly AppDbContext context;
    
        private ICarRepository car;

      public ICarRepository Car => (car == null ? new CarRepository(context) : car);

        //public WrapperRepository()
        //{
        //    this.context = null;
        //}
        public WrapperRepository(AppDbContext context)
        {
            this.context = context;

            this.context.Database.EnsureCreated();
        }
        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
