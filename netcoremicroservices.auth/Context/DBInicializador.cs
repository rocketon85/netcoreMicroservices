using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCoreMicroservices.Commons.Structures.SecurityStructure;
using System.Security.Claims;

namespace NetCoreMicroservices.Auth.Context
{
    public interface IDBInitializer
    {
        Task Ini();
    }

    public class DBInitializer : IDBInitializer
    {
        private readonly AppDbContext db;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DBInitializer(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext db)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.db = db;
        }
        public async Task Ini()
        {
            IdentityRole role;

            await CreateRoles();
            role = await roleManager.FindByNameAsync(RoleStructure.Admin);
            await AddInitClaimsAdmin(role);
            role = await roleManager.FindByNameAsync(RoleStructure.User);
            await AddInitClaimsUser(role);

            await CreateUserAdmin();
            await CreateUser();
        }

        public async Task CreateRoles()
        {
            if (!await roleManager.RoleExistsAsync(RoleStructure.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(RoleStructure.Admin));
            }
            if (!await roleManager.RoleExistsAsync(RoleStructure.User))
            {
                await roleManager.CreateAsync(new IdentityRole(RoleStructure.User));
            }
        }

        private async Task AddInitClaimsAdmin(IdentityRole role)
        {
            await AddClaim(role, new Claim(CustomClaimTypeStructure.Permission, PermissionStructure.Users.View));
            await AddClaim(role, new Claim(CustomClaimTypeStructure.Permission, PermissionStructure.Users.Edit));
        }

        private async Task AddInitClaimsUser(IdentityRole role)
        {
            await AddClaim(role, new Claim(CustomClaimTypeStructure.Permission, PermissionStructure.Users.View));
        }

        public async Task AddClaim(IdentityRole role, Claim claim)
        {
            var claims = await roleManager.GetClaimsAsync(role);
            if (!claims.Any(c => c.Value == claim.Value))
            {
                await roleManager.AddClaimAsync(role, claim);
            }
        }

        public async Task CreateUserAdmin()
        {
            var user = await userManager.Users.FirstOrDefaultAsync(p => p.UserName == "admin");
            if (user == null)
            {
                user = new IdentityUser() { UserName = "admin", Email = "admin@netcoreservice.com" };
                await userManager.CreateAsync(user, "Admin123*");
            }

            await userManager.AddToRoleAsync(user, RoleStructure.Admin);
        }

        public async Task CreateUser()
        {
            var user = await userManager.Users.FirstOrDefaultAsync(p => p.UserName == "user");
            if (user == null)
            {
                user = new IdentityUser() { UserName = "user", Email = "user@netcoreservice.com" };
                await userManager.CreateAsync(user, "User123*");
            }

            await userManager.AddToRoleAsync(user, RoleStructure.User);
        }
    }
}
