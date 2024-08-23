using System.Security.Claims;
using LittleConqueror.AppService.Domain.Services;

namespace LittleConqueror.Middlewares;

public class UserContextMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IUserContext userContext)
    {
        var user = context.User;

        if (user.Identity?.IsAuthenticated == true)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier) 
                              ?? user.FindFirst("sub") 
                              ?? user.FindFirst("id");

            if (userIdClaim != null && long.TryParse(userIdClaim.Value, out var userId))
            {
                userContext.UserId = userId;
            }
        }

        await next(context);
    }
}