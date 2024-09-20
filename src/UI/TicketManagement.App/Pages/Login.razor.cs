using Microsoft.AspNetCore.Components;
using TicketManagement.App.Contracts;
using TicketManagement.App.ViewModels;

namespace TicketManagement.App.Pages;

public partial class Login
{
    public LoginViewModel LoginViewModel { get; set; }

    [Inject] public NavigationManager NavigationManager { get; set; }

    public string Message { get; set; }

    [Inject] private IAuthenticationService AuthenticationService { get; set; }

    protected override void OnInitialized()
    {
        LoginViewModel = new LoginViewModel();
    }

    protected async void HandleValidSubmit()
    {
        if ((await AuthenticationService.Login(LoginViewModel.Email, LoginViewModel.Password)).Success)
            NavigationManager.NavigateTo("/", true);
        else
            Message = "Username/password combination unknown";
    }
}