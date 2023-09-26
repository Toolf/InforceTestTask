using Microsoft.AspNetCore.Mvc;
using webapi.Core.Pagination;
using webapi.Database.Interface;
using webapi.Domain.Entity.ShortUrl;
namespace webapi.Controller;

[ApiController]
[Route("[controller]")]
public class ShortUrlController : ControllerBase
{
    private readonly ILogger<ShortUrlController> _logger;
    private readonly IShortUrlRepository _urlRepository;

    public ShortUrlController(ILogger<ShortUrlController> logger, IShortUrlRepository urlRepository)
    {
        _logger = logger;
        _urlRepository = urlRepository;
    }

    [HttpGet("redirect/{shortUrl}")]
    public ActionResult Get(string shortUrl)
    {
        var url = _urlRepository.GetUrl(shortUrl);
        if (url == null)
        {
            return NotFound("Short url not found");
        }
        return Redirect(url);
    }

    [HttpGet("{id}")]
    public ShortUrlInfo Details(long id)
    {
        var shortUrlInfo = _urlRepository.GetById(id);
        return shortUrlInfo;
    }

    [HttpGet("list")]
    public PaginationResponse<ShortUrlShortInfo> List([FromQuery]  PaginationRequest req)
    {
        var res = _urlRepository.List(req);
        return res;
    }

    [HttpPost]
    public long Create(string url)
    {
        ShortUrlCreate shortUrlCreate = new() { Url = url, CreatedBy = 1 };
        long id = _urlRepository.Create(shortUrlCreate, "someurl_" + shortUrlCreate.Url);
        return id;
    }

    [HttpDelete]
    public bool Delete(long id)
    {
        bool res = _urlRepository.Delete(id);
        return res;
    }
}
