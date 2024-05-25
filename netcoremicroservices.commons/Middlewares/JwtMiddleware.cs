using Microsoft.AspNetCore.Http;
using NetCoreMicroservices.Commons.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreMicroservices.Commons.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJwtService jwtService)
        {
            var token = (context.Request.Headers["Bearer"].FirstOrDefault() != null ? context.Request.Headers["Bearer"].FirstOrDefault()  : context.Request.Headers["Authorization"].FirstOrDefault())?.Split(" ").Last();
            var payload = jwtService.ValidateJwtToken(token);
            if (payload != null)
                context.Items["User"] = payload;


            await _next(context);
        }
    }
}
