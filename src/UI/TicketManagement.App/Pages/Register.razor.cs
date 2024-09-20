using Microsoft.AspNetCore.Components;
using TicketManagement.App.Contracts;
using TicketManagement.App.ViewModels;

namespace TicketManagement.App.Pages;

public partial class Register
{
    public RegisterViewModel RegisterViewModel { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    public string Message { get; set; }
    [Inject] private IAuthenticationService AuthenticationService { get; set; }

    protected override void OnInitialized()
    {
        RegisterViewModel = new RegisterViewModel();
    }

    protected async void HandleValidSubmit()
    {
        await AuthenticationService.Register(RegisterViewModel.Email, RegisterViewModel.Password);

        NavigationManager.NavigateTo("home");
    }
}