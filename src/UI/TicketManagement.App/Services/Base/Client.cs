namespace TicketManagement.App.Services;

public partial class Client : IClient
{
    public HttpClient HttpClient { get; }
}