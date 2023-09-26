namespace webapi.Domain.Entity.ShortUrl;

public class ShortUrlShortInfo
{
    public long Id { get; set; }
    public required string ShortUrl { get; set; }
    public required string Url { get; set; }
    public required long CreatedBy { get; set; }
}