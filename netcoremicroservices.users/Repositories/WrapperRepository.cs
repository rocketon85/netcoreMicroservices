using Microsoft.EntityFrameworkCore;
using NetCoreMicroservices.Users.Context;

namespace NetCoreMicroservices.Users.Repositories
{
    public class WrapperRepository : IWrapperRepository
    {
        private readonly AppDbContext context;
    
        private IUserRepository user;

      public IUserRepository User => (user== null ? new UserRepository(context) : user);

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
