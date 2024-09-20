namespace TicketManagement.App.Components;

public class PaginatedList<T>
{
    public PaginatedList()
    {
    }

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        Items = new List<T>();
        Items.AddRange(items);
    }

    public int PageIndex { get; set; }
    public int TotalPages { get; set; }

    public List<T> Items { get; set; }

    public bool HasPreviousPage
    {
        get => PageIndex > 1;
        set { }
    }

    public bool HasNextPage
    {
        get => PageIndex < TotalPages;
        set { }
    }
}