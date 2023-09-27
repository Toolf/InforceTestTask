namespace webapi.Domain.Interface.ShortUrl;

public interface IShortUrlService
{
    public string GenerateShortUrl(string url);
}