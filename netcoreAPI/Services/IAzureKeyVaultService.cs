using netcoreAPI.Identity;
using System.Security.Claims;

namespace netcoreAPI.Services
{
    public interface IAzureKeyVaultService
    {
        public string GetSecret(string secret);
    }
}
