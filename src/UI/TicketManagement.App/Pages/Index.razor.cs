using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using TicketManagement.App.Auth;
using TicketManagement.App.Contracts;

namespace TicketManagement.App.Pages;

public partial class Index
{
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    [Inject] public NavigationManager NavigationManager { get; set; }

    [Inject] public IAuthenticationService AuthenticationService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await ((CookieAuthenticationStateProvider)AuthenticationStateProvider).GetAuthenticationStateAsync();
    }

    protected void NavigateToLogin()
    {
        NavigationManager.NavigateTo("login");
    }

    protected void NavigateToRegister()
    {
        NavigationManager.NavigateTo("register");
    }

    protected async void Logout()
    {
        await AuthenticationService.Logout();
        NavigationManager.NavigateTo("/", true);
    }
}