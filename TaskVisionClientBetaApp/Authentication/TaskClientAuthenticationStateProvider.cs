using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TaskVisionClientBetaApp.Authentication
{
	public class TaskClientAuthenticationStateProvider : AuthenticationStateProvider
	{
		public static bool IsAuthenticated { get; set; }
		public static bool IsAuthenticating { get; set; }

		public static string Username { get; set; }

		public override Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			ClaimsIdentity identity;

			if (IsAuthenticating)
			{
				return null;
			}
			else if (IsAuthenticated)
			{
				var claims = new List<Claim>();
				claims.Add(new Claim("IsAdmin", "True"));
				
				identity = new ClaimsIdentity(claims, "IsAdmin");

			}
			else
			{
				identity = new ClaimsIdentity();
			}


			return  Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
		}

		public void NotifyAuthenticationStateChanged()
		{
			NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
		}
	}
}
