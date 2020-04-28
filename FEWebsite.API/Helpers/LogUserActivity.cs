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
            var resultContext = await next().ConfigureAwait(false);
            var actionsToSkip = new List<string>()
            {
                nameof(UserController) + "." + nameof(UserController.GetGenders)
            };

            var currentAction = context.ActionDescriptor.DisplayName;
            bool actionShouldBeSkipped = actionsToSkip.Any(action => currentAction.Contains(action));
            if (actionShouldBeSkipped)
            {
                return;
            }

            var userId = int.Parse(resultContext.HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier).Value);
            var userRepoService = resultContext.HttpContext.RequestServices.GetService<IUserRepoService>();
            var unitOfWork = resultContext.HttpContext.RequestServices.GetService<IUnitOfWork>();
            var user = await userRepoService.GetUser(userId).ConfigureAwait(false);

            user.LastLogin = DateTime.Now;
            await unitOfWork.SaveAllAsync().ConfigureAwait(false);
        }
    }
}
