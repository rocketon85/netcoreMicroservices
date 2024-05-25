using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreMicroservices.Auth.Models;
using NetCoreMicroservices.Commons.Services;
using System.Security.Claims;
using System.Text.Json;

namespace NetCoreMicroservices.Auth.Repositories
{
    public interface IAuthRepository 
    {
        Task<string> Authenticate(LoginModel model,
            IJwtService jwtService,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager,
            CancellationToken cancellation = default(CancellationToken));
    }

    public class AuthRepository: IAuthRepository
    {
        public AuthRepository()
        {
            
        }

        public async Task<string> Authenticate(LoginModel model,
            IJwtService jwtService,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager,
            CancellationToken cancellation = default)
        {
            try
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var user = await userManager.Users.Where(p => p.UserName == model.UserName).FirstAsync();
                    var roles = await userManager.GetRolesAsync(user);

                    List<Claim> claims = new List<Claim>();
                        foreach (var role in roleManager.Roles.Where(p => roles.Contains(p.Name)).ToList())
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                        var roleClaims = await roleManager.GetClaimsAsync(role);
                        claims.AddRange(roleClaims);
                    }
                    
                    return jwtService.GenerateJwtToken(user.Id, claims.ToArray());
                }
            }
            catch (Exception ex)
            {

            }
            return default;
        }
    }
}
