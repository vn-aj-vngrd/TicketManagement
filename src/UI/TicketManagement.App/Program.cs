using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TicketManagement.App;
using TicketManagement.App.Auth;
using TicketManagement.App.Contracts;
using TicketManagement.App.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<CookieHandler>();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();

builder.Services.AddTransient<CookieAuthenticationStateProvider>();

builder
    .Services
    .AddHttpClient<IClient, Client>(
        client => client.BaseAddress = new Uri("https://localhost:7081/")
    )
    .AddHttpMessageHandler<CookieHandler>();

builder
    .Services
    .AddHttpClient(
        "Authentication",
        client => client.BaseAddress = new Uri("https://localhost:7081")
    )
    .AddHttpMessageHandler<CookieHandler>();

builder.Services.AddScoped<IEventDataService, EventDataService>();
builder.Services.AddScoped<ICategoryDataService, CategoryDataService>();
builder.Services.AddScoped<IOrderDataService, OrderDataService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

await builder.Build().RunAsync();
