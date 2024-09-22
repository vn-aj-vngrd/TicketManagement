using TicketManagement.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TicketManagement.Identity
{
    public class TicketIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public TicketIdentityDbContext()
        {

        }

        public TicketIdentityDbContext(DbContextOptions<TicketIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
        .LogTo(Console.WriteLine)
        .EnableSensitiveDataLogging();

    }
}
