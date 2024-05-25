using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreMicroservices.Commons.Services
{
    public interface IJwtService
    {
        public string GenerateJwtToken(string payload, Claim[] claims);
        public string? ValidateJwtToken(string? token);
    }
}
