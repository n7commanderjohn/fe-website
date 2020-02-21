using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

using FEWebsite.API.Controllers;
using FEWebsite.API.Data.BaseServices;

namespace FEWebsite.API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var actionsToSkip = new List<string>() {
                nameof(UsersController) + "." + nameof(UsersController.GetGenders)
            };

            var currentAction = context.ActionDescriptor.DisplayName;
            bool actionShouldBeSkipped = actionsToSkip.Any(action => currentAction.Contains(action));
            if (actionShouldBeSkipped) {
                return;
            }

            var resultContext = await next().ConfigureAwait(false);

            var userId = int.Parse(resultContext.HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier).Value);
            var userService = resultContext.HttpContext.RequestServices.GetService<IUsersService>();
            var user = await userService.GetUser(userId).ConfigureAwait(false);

            user.LastLogin = DateTime.Now;
            await userService.SaveAll().ConfigureAwait(false);
        }
    }
}
