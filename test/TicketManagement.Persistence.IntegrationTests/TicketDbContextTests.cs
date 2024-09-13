using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using TicketManagement.Application.Contracts;
using TicketManagement.Domain.Entities;

namespace TicketManagement.Persistence.IntegrationTests;

public class TicketDbContextTests
{
    private readonly string _loggedInUserId;
    private readonly Mock<ILoggedInUserService> _loggedInUserServiceMock;
    private readonly TicketDbContext _ticketDbContext;

    public TicketDbContextTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<TicketDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        _loggedInUserId = "00000000-0000-0000-0000-000000000000";
        _loggedInUserServiceMock = new Mock<ILoggedInUserService>();
        _loggedInUserServiceMock.Setup(m => m.UserId).Returns(_loggedInUserId);

        _ticketDbContext = new TicketDbContext(dbContextOptions, _loggedInUserServiceMock.Object);
    }

    [Fact]
    public async void Save_SetCreatedByProperty()
    {
        var ev = new Event { EventId = Guid.NewGuid(), Name = "Test event" };

        _ticketDbContext.Events.Add(ev);
        await _ticketDbContext.SaveChangesAsync();

        ev.CreatedBy.ShouldBe(_loggedInUserId);
    }
}