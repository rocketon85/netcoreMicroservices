using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetCoreMicroservices.Commons.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreMicroservices.Commons.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtOption configJwt;

        public JwtService(IOptions<JwtOption> configJwt)
        {
            this.configJwt = configJwt.Value;

            if (string.IsNullOrEmpty(this.configJwt.Key))
                throw new Exception("JWT secret not configured");
        }

        public string GenerateJwtToken(string payload, Claim[] claims)
        {
            try
            {
                // generate token that is valid for 7 days
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(configJwt.Key!);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.UserData, payload) ,
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    }.Concat(claims)),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string? ValidateJwtToken(string? token)
        {
            if (token == null)
                return default;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configJwt.Key!);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                return jwtToken.Claims.First(x => x.Type == ClaimTypes.UserData).Value;
            }
            catch
            {
                return default;
            }
        }
    }
}
