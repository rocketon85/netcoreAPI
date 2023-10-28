using netcoreAPI.Identity;
using System.Security.Claims;

namespace netcoreAPI.Services
{
    public interface IJwtService
    {
        public string GenerateJwtToken(User user, Claim[] claims);
        public int? ValidateJwtToken(string? token);
    }
}
