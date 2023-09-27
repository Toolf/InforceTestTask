using webapi.Core.Pagination;
using webapi.Domain.Entity.ShortUrl;

namespace webapi.Database.Interface;

public interface IShortUrlRepository
{
    public long Create(ShortUrlCreate shortUrlCreate, string shortUrl);
    public ShortUrlInfo FindById(long id);
    public string? GetUrl(string shortUrl);
    public bool IsCreatedBy(long id, long userId);
    public bool Delete(long id);
    public PaginationResponse<ShortUrlShortInfo> List(PaginationRequest req);
}