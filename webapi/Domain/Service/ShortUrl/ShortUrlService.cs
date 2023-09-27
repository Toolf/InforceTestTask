using webapi.Domain.Interface.ShortUrl;

namespace webapi.Domain.Service.ShortUrl;

public class ShortUrlService : IShortUrlService
{
    public string GenerateShortUrl(string url)
    {
        Guid uuid = Guid.NewGuid();
        string uuidAsString = uuid.ToString();
        return uuidAsString;
    }
}