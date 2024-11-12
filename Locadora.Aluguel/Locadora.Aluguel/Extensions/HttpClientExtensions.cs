using System.Text;
using System.Text.Json;
using Locadora.Aluguel.Controllers.Infra;

namespace Locadora.Aluguel.Extensions
{
    public static class HttpClientExtensions
    {
        private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        #region GET
        public static async Task<RespostaBase<T?>> GetAsync<T>(this HttpClient client, string requestUri) where T : class
        {
            var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<RespostaBase<T>>(SerializerOptions);
        }

        #endregion

        #region POST
        public static async Task<TResponse?> PostAsync<TRequest, TResponse>(this HttpClient client, string requestUri, TRequest content)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(content, SerializerOptions), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUri, httpContent);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>(SerializerOptions);
        }
        #endregion

        #region PUT
        public static async Task<TResponse?> PutAsync<TRequest, TResponse>(this HttpClient client, string requestUri, TRequest content)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(content, SerializerOptions), Encoding.UTF8, "application/json");
            var response = await client.PutAsync(requestUri, httpContent);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>(SerializerOptions);
        }
        #endregion

        #region PATCH
        public static async Task<TResponse?> PatchAsync<TRequest, TResponse>(this HttpClient client, string requestUri, TRequest content)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(content, SerializerOptions), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Patch, requestUri) { Content = httpContent };
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>(SerializerOptions);
        }
        #endregion

        #region DELETE
        public static async Task<bool> DeleteAsync(this HttpClient client, string requestUri)
        {
            var response = await client.DeleteAsync(requestUri);
            return response.IsSuccessStatusCode;
        }
        #endregion
    }
}
