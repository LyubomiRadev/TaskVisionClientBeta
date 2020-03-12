using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskVisionClientBetaApp.Policies
{
    public static class Policies
    {
        public const string IsAdmin = "IsAdmin";
        public const string IsDeveloper = "IsDeveoper";
        public const string IsTester = "IsTester";

        public static AuthorizationPolicy IsAdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireRole("IsAdmin")
                                                   .Build();
        }

        public static AuthorizationPolicy IsDeveloperPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireRole("Developer")
                                                   .Build();
        }

        public static AuthorizationPolicy IsTesterPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireRole("Tester")
                                                   .Build();
        }

        public class IsAdminRequirement : IAuthorizationRequirement
        {
            public bool IsAdmin { get; set; }

            public IsAdminRequirement(bool isAdmin)
            {
                this.IsAdmin = isAdmin;
            }
        }
    }
}
