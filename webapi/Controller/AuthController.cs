using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webapi.Domain.Entity.Auth;
using webapi.Domain.Interface;
using webapi.Domain.Interface.Auth;

namespace webapi.Controller;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<ShortUrlController> _logger;
    private readonly IAuthService _authService;
    private readonly IJwtService _jwtService;

    public AuthController(ILogger<ShortUrlController> logger, IAuthService authService, IJwtService jwtService)
    {
        _logger = logger;
        _authService = authService;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login(Credentials credentials)
    {
        var user = _authService.AuthenticateUser(credentials);

        if (user == null)
        {
            return Unauthorized(new { Message = "Invalid username or password" });
        }

        // Generate JWT token and return it
        var token = _jwtService.GenerateToken(user.Id.ToString(), user.Role);
        return Ok(new { Token = token });
    }

    [HttpPost("registry")]
    public IActionResult Registry(string username, string password)
    {
        RegistrationCredentials registrationCredentials = new() 
        {
            Username = username,
            Password = password,
            Role = "user",
        };
        var isRegistered = _authService.Registry(registrationCredentials);

        if (isRegistered)
        {
            return Ok(new { Message = "Registration successful" });
        }
        else
        {
            return BadRequest(new { Message = "User already exists" });
        }
    }

    [HttpGet("profile")]
    [Authorize]
    public IActionResult UserProfile()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        var userRoleClaim = User.FindFirst(ClaimTypes.Role);

        if (userIdClaim != null && userRoleClaim != null)
        {
            var userId = userIdClaim.Value;
            var userRole = userRoleClaim.Value;

            return Ok(new { UserId = userId, UserRole = userRole });
        }
        else
        {
            return Unauthorized();
        }
    }
}
