namespace StudentCouncilTracker.Application.Entities.Base.Filters;

public class ListFilter
{
    private int? _page;

    public string query { get; set; }

    public int page
    {
        get => _page ?? 1;
        set => _page = value;
    }
}