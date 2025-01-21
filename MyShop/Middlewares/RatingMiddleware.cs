using Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Services;
using System.Threading.Tasks;

namespace MyShop.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RatingMiddleware
    {
        private readonly RequestDelegate _next;

        public RatingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext,IRatingService ratingService)
        {
            Rating rating = new() { 
                Host=httpContext.Request.Host.ToString(),
                Method= httpContext.Request.Method.ToString(),
                Path=httpContext.Request.Path.ToString(),
                Referer=httpContext.Request.Headers.Referer.ToString(),
                UserAgent=httpContext.Request.Headers.UserAgent.ToString(),
                RecordDate=DateTime.Now,
                
            };
         await ratingService.createRating(rating);
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RatingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRatingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RatingMiddleware>();
        }
    }
}
