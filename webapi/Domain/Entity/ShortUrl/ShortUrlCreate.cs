namespace webapi.Domain.Entity.ShortUrl;

public class ShortUrlCreate
{
    public required string Url { get; set; }
    public required long CreatedBy { get; set; }
}