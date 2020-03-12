using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static TaskVisionClientBetaApp.Policies.Policies;

namespace TaskVisionClientBetaApp.Authentication
{
    public class IsAdminHandler : AuthorizationHandler<IsAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminRequirement requirement)
        {

            if (!context.User.HasClaim(c => c.Type == "IsAdmin" && c.Value == "True" ))
            {
                return Task.CompletedTask;
            }

            var isUserAdminString = context.User.FindFirst( u => u.Type == "IsAdmin").Value;

            var isUserAdminValue = false;

            if (Boolean.TryParse(isUserAdminString, out isUserAdminValue))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
