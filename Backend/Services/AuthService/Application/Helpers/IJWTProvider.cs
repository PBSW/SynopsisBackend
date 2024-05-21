using System.Security.Claims;

namespace Application.Helpers;

public interface IJWTProvider
{
    public string GenerateToken(int id, string username, IEnumerable<Claim> additionalClaims = null);
}