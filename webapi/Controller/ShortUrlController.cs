using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
    [Authorize]
    public ShortUrlInfo Details(long id)
    {
        var shortUrlInfo = _urlRepository.FindById(id);
        return shortUrlInfo;
    }

    [HttpGet("list")]
    [AllowAnonymous]
    public PaginationResponse<ShortUrlShortInfo> List([FromQuery]  PaginationRequest req)
    {
        var res = _urlRepository.List(req);
        return res;
    }

    [HttpPost]
    [Authorize]
    public ActionResult Create(string url)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdClaim?.Value, out int userId))
        {
            return Unauthorized();
        }

        ShortUrlCreate shortUrlCreate = new() { Url = url, CreatedBy = userId };
        long id = _urlRepository.Create(shortUrlCreate, "someurl_" + shortUrlCreate.Url);
        return Ok(id);
    }

    [HttpDelete]
    [Authorize]
    public ActionResult Delete(long id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        var userRoleClaim = User.FindFirst(ClaimTypes.Role);

        if (userIdClaim != null && userRoleClaim != null)
        {
            var userRole = userRoleClaim.Value;
            if (!int.TryParse(userIdClaim.Value, out int userId)) return Unauthorized();
            if (userRole == "admin" || _urlRepository.IsCreatedBy(id, userId))
            {
                bool res = _urlRepository.Delete(id);
                return Ok(res);
            } else {
                return BadRequest("No access");
            }
        }
        else
        {
            return Unauthorized();
        }
    }
}
