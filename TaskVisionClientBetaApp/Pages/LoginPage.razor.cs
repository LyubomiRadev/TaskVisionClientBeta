using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;
using TaskVisionClientBetaApp.Authentication;

namespace TaskVisionClientBetaApp.Page
{

	public class LoginPageBase : ComponentBase
	{
		#region Fields

		[Inject]
		protected NavigationManager NavigationManager { get; set; }

		[Inject]
		IAuthorizationService AuthorizationService { get; set; }

		#endregion End Fields

		#region OnInitialization

		protected override async Task OnInitializedAsync()
		{
			this.SetLogInRegisterButtonsClasses();
			await Task.Delay(1000);
		}

		#endregion OnInitialization


		#region Properties

		public string PageTitle { get; set; }

		public string ShowRegisterForm { get; set; }

		public string ShowLoginForm { get; set; }

		public string LoginTabActivate { get; set; }

		public string RegisterTabActivate { get; set; }

		public string Password { get; set; }

		public string Email { get; set; }


		public string ConfirmedPassword { get; set; }

		[Parameter]
		public string Username { get; set; }


		[CascadingParameter]
		private Task<AuthenticationState> authenticationStateTask { get; set; }

		#endregion End Properties

		#region Methods


		#region SetLogInRegisterButtonsClasses Method

		private void SetLogInRegisterButtonsClasses()
		{
			this.ShowLoginForm = "block";
			this.ShowRegisterForm = "none";
			this.PageTitle = "Login Page";
			this.LoginTabActivate = "active";
			this.RegisterTabActivate = "unactiveButton";
		}

		#endregion End SetLogInRegisterButtonsClasses Method


		#region ClickLogInTab Method

		public void ClickLogInTab()
		{
			this.ShowLoginForm = "block";
			this.LoginTabActivate = "active";

			this.ShowRegisterForm = "none";
			this.RegisterTabActivate = "unactiveButton";
		}

		#endregion End ClickLogInTab Method


		#region ClickRegisterTabMethod Method

		public void ClickRegisterTabMethod()
		{
			this.ShowLoginForm = "none";
			this.LoginTabActivate = "unactiveButton";

			this.ShowRegisterForm = "block";
			this.RegisterTabActivate = "active";
		}

		#endregion End ClickRegisterTabMethod Method


		#region RegisterCommand

		public void RegisterCommand()
		{
			this.DoPasswordsMatch();
		}

		#endregion End RegisterCommand


		#region LoginCommand Method

		public async Task LoginCommand()
		{
			if (this.Username == "Lyubo" && this.Password == "1")
			{
				TaskClientAuthenticationStateProvider.IsAuthenticated = true;

				var stateProvider = new TaskClientAuthenticationStateProvider();
				var authSate = await stateProvider.GetAuthenticationStateAsync();
				var user = authSate.User;

				if (user.Identity.IsAuthenticated)
				{
					var isUserAdmin = (await AuthorizationService.AuthorizeAsync(user, "IsAdmin")).Succeeded;

					if (isUserAdmin)
					{
						user.IsInRole("IsAdmin");
						stateProvider.NotifyAuthenticationStateChanged();
						NavigationManager.NavigateTo("");
					}
				}

				
			}
		}

		#endregion End LoginCommand Method



		#region DoPasswordsMatch Method

		public bool DoPasswordsMatch()
		{
			var doPassowrdsMath = false;

			if (this.Password == this.ConfirmedPassword)
			{
				doPassowrdsMath = true;
			}

			return doPassowrdsMath;

		}

		#endregion End DoPasswordsMatch Method

		#endregion End Methods

	}
}
