using netcoreAPI.Identity;

namespace netcoreAPI.Services
{
    public interface IJwtService
    {
        public string GenerateJwtToken(User user);
        public int? ValidateJwtToken(string? token);
    }
}
