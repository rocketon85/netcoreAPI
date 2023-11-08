using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using netcoreAPI.Identity;
using netcoreAPI.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace netcoreAPI.Services
{
    public class AzureKeyVaultService : IAzureKeyVaultService
    {
        private readonly AzureOption _configAzure;
        private readonly SecretClient _secretsClient;

        public AzureKeyVaultService() { }
        public AzureKeyVaultService(IOptions<AzureOption> configAzure)
        {
#if !DEBUG
            _configAzure = configAzure.Value;
            if (string.IsNullOrEmpty(_configAzure.KeyVaultUrl))
                throw new Exception("KeyVaultURL not configured");
            _secretsClient = new SecretClient(new Uri(_configAzure.KeyVaultUrl), new DefaultAzureCredential());
#endif
        }

        public  string GetSecret(string secret)
        {
#if !DEBUG
            return _secretsClient.GetSecret(secret).Value.Value;
#else
            return "";
#endif
        }
    }
}
