using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace netcoreAPI.Tests.Extensions
{
    public static class HttpClientExtension
    {
        public static async Task<T?> GetFromJsonAsync<T>(this HttpClient httpClient, string url, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            using var httpResponse = await httpClient.SendAsync(request);

            if (httpResponse.IsSuccessStatusCode)
            {
                return await httpResponse.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                string errorMessage = httpResponse.ReasonPhrase ?? "";
                Console.WriteLine($"There was an error! {errorMessage}");
                return default;
            }
        }

        public static async Task<T?> PostAsJsonAsync<T, TVal>(this HttpClient httpClient, string url, TVal postBody)
        {
            using var httpResponse = await httpClient.PostAsJsonAsync(url, postBody);

            if (httpResponse.IsSuccessStatusCode)
            {
                return await httpResponse.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                string errorMessage = httpResponse.ReasonPhrase ?? "";
                Console.WriteLine($"There was an error! {errorMessage}");
                return default;
            }
        }

        public static async Task<T?> PostAsJsonAsync<T, TVal>(this HttpClient httpClient, string url, TVal postBody, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(JsonSerializer.Serialize(postBody), Encoding.UTF8, "application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using var httpResponse = await httpClient.SendAsync(request);

            if (httpResponse.IsSuccessStatusCode)
            {
                return await httpResponse.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                string errorMessage = httpResponse.ReasonPhrase ?? "";
                Console.WriteLine($"There was an error! {errorMessage}");
                return default;
            }
        }
    }
}
