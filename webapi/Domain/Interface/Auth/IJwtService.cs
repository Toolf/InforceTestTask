using System.Security.Claims;

namespace webapi.Domain.Interface;

public interface IJwtService
{
    public string GenerateToken(string userId, string userRole);
    public ClaimsPrincipal ValidateToken(string token);
    public IEnumerable<Claim> GetClaimsFromToken(string token);
}