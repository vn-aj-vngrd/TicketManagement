using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketManagement.Identity.Models;

namespace TicketManagement.Identity
{
    public static class IdentityServiceExtensions
    {
        public static void AddIdentityServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();

            services.AddAuthorizationBuilder();

            services.AddDbContext<TicketIdentityDbContext>(
                options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("TicketIdentityConnectionString")
                    )
            );

            services
                .AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<TicketIdentityDbContext>()
                .AddApiEndpoints();
        }
    }
}
