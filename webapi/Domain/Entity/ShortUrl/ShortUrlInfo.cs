namespace webapi.Domain.Entity.ShortUrl;

public class ShortUrlInfo
{
    public long Id { get; set; }
    public required string ShortUrl { get; set; }
    public required string Url { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required long CreatedBy { get; set; }
}