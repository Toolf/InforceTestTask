using webapi.Core.Pagination;
using webapi.Database.Interface;
using webapi.Database.Model;
using webapi.Domain.Entity.ShortUrl;


namespace webapi.Database.Repository;

public class ShortUrlRepository : IShortUrlRepository
{
    private readonly Db _db;
    public ShortUrlRepository(Db db)
    {
        _db = db;
    }

    /*
        Return -1 if url already exists 
    */
    public long Create(ShortUrlCreate shortUrlCreate, string shortUrl)
    {
        var shortUrlId = _db.ShortUrl
            .Where(e => e.Url == shortUrlCreate.Url)
            .Select(e => e.Id)
            .FirstOrDefault();
        if (shortUrlId == null) return -1;
        ShortUrlModel shortUrlModel = new()
        {
            CreatedAt = DateTime.Now,
            CreatedBy = shortUrlCreate.CreatedBy,
            Url = shortUrlCreate.Url,
            ShortUrl = shortUrl,
        };
        _db.ShortUrl.Add(shortUrlModel);
        _db.SaveChanges();
        return shortUrlModel.Id;
    }

    public bool Delete(long id)
    {
        var shortUrl = _db.ShortUrl.Find(id);
        if (shortUrl == null)
        {
            throw new Exception("Short url not found");
        }
        _db.ShortUrl.Remove(shortUrl);
        _db.SaveChanges();
        return true;
        
    }

    public ShortUrlInfo FindById(long id)
    {
        var shortUrl = _db.ShortUrl
            .Where(e => e.Id == id)
            .Select(e => new ShortUrlInfo
            {
                Id = e.Id,
                ShortUrl = e.ShortUrl,
                Url = e.Url,
                CreatedAt = e.CreatedAt,
                CreatedBy = e.CreatedBy,
            })
            .FirstOrDefault();
        if (shortUrl == null)
        {
            throw new Exception("Short url not found");
        }
        return shortUrl;
    }

    public string? GetUrl(string shortUrl)
    {
        var url = _db.ShortUrl
            .Where(e => e.ShortUrl == shortUrl)
            .Select(e => e.Url)
            .FirstOrDefault();
        return url;
    }

    public bool IsCreatedBy(long id, long userId)
    {
        return  _db.ShortUrl
            .Where(e => e.Id == id)
            .Where(e => e.CreatedBy == userId)
            .Select(e => true)
            .Count() != 0;
     
    }

    public PaginationResponse<ShortUrlShortInfo> List(PaginationRequest req)
    {
        var query = _db.ShortUrl
            .OrderBy(e => e.Id)
            .Select(e => new ShortUrlShortInfo
            {
                Id = e.Id,
                ShortUrl = e.ShortUrl,
                Url = e.Url,
                CreatedBy = e.CreatedBy,
            });

        var totalCount = query.Count();
        var paginatedRecords = query
            .Skip((req.Page - 1) * req.PerPage)
            .Take(req.PerPage)
            .ToList();

        return new()
        {
            Total = totalCount,
            Page = req.Page,
            PerPage = req.PerPage,
            Data = paginatedRecords
        };
    }
}