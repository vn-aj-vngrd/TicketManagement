using TicketManagement.App.Services.Base;

namespace TicketManagement.App.Contracts;

public interface IAuthenticationService
{
    Task<ApiResponse> Login(string email, string password);
    Task<ApiResponse> Register(string email, string password);
    Task Logout();
}