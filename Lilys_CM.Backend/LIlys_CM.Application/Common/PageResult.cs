namespace Lilys_CM.Application.Common;

public class PageResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    // Backward-compatible aliases used by existing frontend paging view-models.
    public int CurrentPage => Page;
    public bool IncludedTotal => true;
    public int TotalItems => TotalCount;
    public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling((double)TotalCount / PageSize);
}
