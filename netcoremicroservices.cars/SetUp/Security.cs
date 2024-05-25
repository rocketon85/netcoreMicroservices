using Microsoft.AspNetCore.Authorization;
using NetCoreMicroservices.Cars.Structures.SecurityStructure;
using NetCoreMicroservices.Commons.Extensions;
using NetCoreMicroservices.Commons.Structures.SecurityStructure;

namespace NetCoreMicroservices.Cars.SetUp
{
    public static class Security
    {
        public static Action<AuthorizationOptions> SetSecurity()
        {
            PolicyTypesStructure policyTypes = new PolicyTypesStructure();
            return options =>
            {
                options.AddPolicy(policyTypes.ViewList, policy => policy.RequireAssertion(context =>
                {
                    return context.User.IsInRole(RoleStructure.Admin) || context.User.HasClaim(c => c.Value == PermissionStructure.Cars.ViewList);
                }));
                options.AddPolicy(policyTypes.ViewDetail, policy => policy.RequireAssertion(context =>
                {
                    return context.User.IsInRole(RoleStructure.Admin) || context.User.HasClaim(c => c.Value == PermissionStructure.Cars.ViewDetail);
                }));
                options.AddPolicy(policyTypes.Add, policy => policy.RequireAssertion(context =>
                {
                    return context.User.IsInRole(RoleStructure.Admin) || context.User.HasClaim(c => c.Value == PermissionStructure.Cars.Add);
                }));
                options.AddPolicy(policyTypes.Edit, policy => policy.RequireAssertion(context =>
                {
                    return context.User.IsInRole(RoleStructure.Admin) || context.User.HasClaim(c => c.Value == PermissionStructure.Cars.Edit);
                }));
                options.AddPolicy(policyTypes.Delete, policy => policy.RequireAssertion(context =>
                {
                    return context.User.IsInRole(RoleStructure.Admin) || context.User.HasClaim(c => c.Value == PermissionStructure.Cars.Delete);
                }));
            };
        }
    }
}
