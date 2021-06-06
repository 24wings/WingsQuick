using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Shared.Dto;
using Wings.Examples.UseCase.Client.Pages;
using Wings.Examples.UseCase.Client.Services;

namespace Wings.Examples.UseCase.Client.Pages
{
 public   enum PageStatus
    {
        Login,
        Register,
        Password
    }
 
    public class LoginBase:ComponentBase
    {
      public  PageStatus status = PageStatus.Login;

        protected void OnFinishFailed(EditContext editContext)
        {
            Console.WriteLine($"Failed:{JsonSerializer.Serialize(loginModel)}");
        }
        protected LoginModel loginModel = new LoginModel();
        protected bool ShowErrors;
        protected string Error = "";
        [Inject]
        protected NavigationManager navigationManager { get; set; }
        [Inject]
        protected IAuthService authService { get; set; }

        protected async Task HandleLogin()
        {
            ShowErrors = false;

            var result = await authService.Login(loginModel);

            if (result.Successful)
            {
                navigationManager.NavigateTo("/");
            }
            else
            {
                Error = result.Error;
                ShowErrors = true;
            }
        }
        protected RegisterModel registerModel = new RegisterModel();
        protected bool ShowRegisterErrors;
        protected IEnumerable<string> Errors;

        protected async Task HandleRegistration()
        {
            ShowRegisterErrors = false;

            var result = await authService.Register(registerModel);

            if (result.Successful)
            {
                //NavigationManager.NavigateTo("/login");
            }
            else
            {
                Errors = result.Errors;
                ShowErrors = true;
            }
        }
    }
}
