using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using netcoreAPI.Domains;
using netcoreAPI.Extensions;
using netcoreAPI.Identity;
using netcoreAPI.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace netcoreAPI.Services
{
    public class AzureFuncService : IAzureFuncService 
    {
        private readonly AzureOption _configAzure;
        private readonly SecretClient _secretsClient;

        private readonly HttpClient _httpClient;

        public AzureFuncService() { }
        public AzureFuncService(IOptions<AzureOption> configAzure)
        {
            _httpClient = new HttpClient();
            _configAzure= configAzure.Value;
        }

        public async Task<string> FuncNewCar(CarDomain data)
        {
            var response = await _httpClient.PostAsJsonAsync<dynamic,CarDomain>($"{_configAzure.FunctionsUrl.NewCar}", data);
            if (!response.IsSuccessStatusCode) return "";
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> FuncGetUserDetail(int userId)
        {
            var response = await _httpClient.GetAsync($"{_configAzure.FunctionsUrl.GetUserDetail}?userId={userId}");
            if (!response.IsSuccessStatusCode) return "";
            return await response.Content.ReadAsStringAsync();
        }
    }
}
