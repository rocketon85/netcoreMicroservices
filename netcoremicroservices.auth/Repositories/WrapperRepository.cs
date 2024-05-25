using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCoreMicroservices.Commons.Services;
using NetCoreMicroservices.Auth.Context;

namespace NetCoreMicroservices.Auth.Repositories
{
    public class WrapperRepository : IWrapperRepository
    {
        private readonly AppDbContext context;
        private readonly IJwtService jwtService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<IdentityUser> signInManager;

        private IAuthRepository auth;

        public WrapperRepository(AppDbContext context, 
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager,
            IJwtService jwtService)
        {
            this.context = context;
            this.jwtService = jwtService;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;

            this.context.Database.EnsureCreated();
        }

        public IAuthRepository Auth
        {
            get
            {
                if (auth != null)
                    return auth;

                auth = new AuthRepository();
                return auth;
            }
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
