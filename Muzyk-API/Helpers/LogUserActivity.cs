using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Muzyk_API.Data;

namespace Muzyk_API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            var userId = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var repo = resultContext.HttpContext.RequestServices.GetService<IMuzykRepository>();
            var user = await repo.GetUser(userId);
            user.LastActive = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local);
            await repo.SaveAll();
        }
    }
}