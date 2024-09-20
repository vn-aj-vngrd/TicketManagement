using TicketManagement.App.ViewModels;

namespace TicketManagement.App.Contracts;

public interface IOrderDataService
{
    Task<PagedOrderForMonthViewModel> GetPagedOrderForMonth(DateTime date, int page, int size);
}