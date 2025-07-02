using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace MyShop.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _jwtSecret;
        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _jwtSecret = configuration["Jwt:Key"];
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // שליפת הטוקן מהקוקי והגשתו ל-JwtBearer
            var token = context.Request.Cookies["jwtToken"];
            if (!string.IsNullOrEmpty(token))
            {
                context.Request.Headers["Authorization"] = $"Bearer {token}";
            }
            await _next(context);
        }
    }
}
