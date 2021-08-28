using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebServer.Db.Context;
using WebServer.Internal.Extensions;

namespace WebServer.Internal.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, PhotosDbContext dbContext)
        {
            var token = context.Request.Headers["Token"].ToString();
            var hash = token.ToMD5Hash();

            var exist = await dbContext.UserTokens.AsNoTracking()
                .AnyAsync(ut => ut.TokenHash == hash);
            if (exist)
            {
                await _next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token not found");
            }
        }
    }
}