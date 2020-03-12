using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskVisionClientBetaApp.Pages
{
    public class RegisterPageBase : ComponentBase
    {

        #region Properties

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public string ConfirmedPassword { get; set; }

        #endregion End Properties

        #region Methods

        public void RegisterCommand()
        {
            this.DoPasswordsMatch();
        }


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
