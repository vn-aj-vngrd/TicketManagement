﻿using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Api.Middleware;
using TicketManagement.Api.Services;
using TicketManagement.Application;
using TicketManagement.Application.Contracts;
using TicketManagement.Identity;
using TicketManagement.Identity.Models;
using TicketManagement.Infrastructure;
using TicketManagement.Persistence;

namespace TicketManagement.Api;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddPersistenceServices(builder.Configuration);
        builder.Services.AddIdentityServices(builder.Configuration);

        builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddControllers();

        builder
            .Services
            .AddCors(
                options =>
                    options.AddPolicy(
                        "open",
                        policy =>
                            policy
                                .WithOrigins(
                                    builder.Configuration["ApiUrl"] ?? "https://localhost:7020",
                                    builder.Configuration["BlazorUrl"] ?? "https://localhost:7080"
                                )
                                .AllowAnyMethod()
                                .SetIsOriginAllowed(pol => true)
                                .AllowAnyHeader()
                                .AllowCredentials()
                    )
            );

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.MapIdentityApi<ApplicationUser>();

        app.MapPost(
            "/Logout",
            async (ClaimsPrincipal user, SignInManager<ApplicationUser> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return TypedResults.Ok();
            }
        );

        app.UseCors("open");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCustomExceptionHandler();

        app.UseHttpsRedirection();
        app.MapControllers();

        return app;
    }

    public static async Task ResetDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        try
        {
            var context = scope.ServiceProvider.GetService<TicketDbContext>();
            if (context != null)
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            // Add logging here later on
            Console.WriteLine(ex.Message);
        }
    }
}
