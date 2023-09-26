namespace webapi.Core.Pagination;

public class PaginationResponse<Entity>
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }
    public required List<Entity> Data { get; set; }
    public int Count => Data.Count;
}