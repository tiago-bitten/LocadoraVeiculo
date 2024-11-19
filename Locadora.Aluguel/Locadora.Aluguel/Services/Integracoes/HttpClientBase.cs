using Locadora.Aluguel.Controllers.Infra;

namespace Locadora.Aluguel.Services.Integracoes;

public class HttpClientBase
{
    #region Ctor 
    private readonly HttpClient _httpClient;

    public HttpClientBase(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    #endregion

    #region GetAsync
    protected async Task<RespostaBase<T>> GetAsync<T>(string url) where T : class
    {
        var response = await _httpClient.GetAsync(url);
        return await ProcessResponse<T>(response);
    }
    #endregion

    #region PostAsync
    protected async Task<RespostaBase<T>> PostAsync<T>(string url, object content) where T : class
    {
        var response = await _httpClient.PostAsJsonAsync(url, content);
        return await ProcessResponse<T>(response);
    }
    #endregion

    #region PutAsync
    protected async Task<RespostaBase<T>> PutAsync<T>(string url, object content) where T : class
    {
        var response = await _httpClient.PutAsJsonAsync(url, content);
        return await ProcessResponse<T>(response);
    }
    #endregion

    #region DeleteAsync
    protected async Task<RespostaBase<T>> DeleteAsync<T>(string url) where T : class
    {
        var response = await _httpClient.DeleteAsync(url);
        return await ProcessResponse<T>(response);
    }
    #endregion

    #region ProcessResponse
    private async Task<RespostaBase<T>> ProcessResponse<T>(HttpResponseMessage response) where T : class
    {
        var result = new RespostaBase<T> { Sucesso = response.IsSuccessStatusCode };

        if (response.IsSuccessStatusCode)
        {
            result.Conteudo = await response.Content.ReadFromJsonAsync<T>();
        }
        else
        {
            result.Mensagem = response.ReasonPhrase;
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                result.Mensagem = !string.IsNullOrWhiteSpace(errorContent) ? errorContent : result.Mensagem;
            }
        }

        return result;
    }
    #endregion
}