using MediatR;

namespace TicketManagement.Application.Features.Events.Queries.GetEventsList;

public class GetEventsListQuery : IRequest<List<EventListVm>> { }
