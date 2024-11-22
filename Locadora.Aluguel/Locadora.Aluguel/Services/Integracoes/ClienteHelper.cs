using Locadora.Aluguel.Enterprise;

namespace Locadora.Aluguel.Services.Integracoes;

#region Interface
public interface IClienteHelper
{
    Task<ResultadoClienteDto> ObterPorIdAsync(string codigoCliente);
    Task<List<ResultadoClienteDto>> ObterTodosAsync();
    Task<ClienteValidoDto> ValidarClienteAsync(string codigoCliente);
}
#endregion

public class ClienteHelper : HttpClientBase, IClienteHelper
{
    #region Ctor
    private const string UrlBase = "http://localhost:5009/api";
    private const string ActionObterClientePorId = "/Clientes/{0}";
    private const string ActionObterTodos = "/Clientes";
    private const string ActionValidarParaAlugar = "/Clientes/ValidarParaAlugar/{0}";

    public ClienteHelper(HttpClient httpClient) : base(httpClient)
    {
    }
    #endregion
    
    #region ObterTodosAsync
    public async Task<List<ResultadoClienteDto>> ObterTodosAsync()
    {
        var url = $"{UrlBase}{ActionObterTodos}";
        var resposta = await GetAsync<List<ResultadoClienteDto>>(url);

        if (resposta.Sucesso)
            return resposta.Conteudo;

        var mensagem = resposta?.Mensagem ?? "Sem resposta do serviço de cliente";
        throw new AluguelAppException(ETipoException.ErroIntegracaoCliente, mensagem);
    }
    #endregion

    #region ObterPorIdAsync
    public async Task<ResultadoClienteDto> ObterPorIdAsync(string codigoCliente)
    {
        var url = $"{UrlBase}{string.Format(ActionObterClientePorId, codigoCliente)}";
        var resposta = await GetAsync<ResultadoClienteDto>(url);

        if (resposta.Sucesso)
            return resposta.Conteudo;

        var mensagem = resposta?.Mensagem ?? "Sem resposta do serviço de cliente";
        throw new AluguelAppException(ETipoException.ErroIntegracaoCliente, mensagem);
    }
    #endregion
    
    #region ValidarClienteAsync
    public async Task<ClienteValidoDto> ValidarClienteAsync(string codigoCliente)
    {
        var url = $"{UrlBase}{string.Format(ActionValidarParaAlugar, codigoCliente)}";
        var resposta = await GetAsync<ClienteValidoDto>(url);

        if (resposta.Sucesso)
            return resposta.Conteudo;

        var mensagem = resposta?.Mensagem ?? "Sem resposta do serviço de cliente";
        throw new AluguelAppException(ETipoException.ErroIntegracaoCliente, mensagem);
    }
    #endregion
}

#region Dtos

#region ResultadoClienteDto
public record ResultadoClienteDto(
    string Id,
    string Nome,
    string Cpf,
    string Email,
    DateTime DataNascimento,
    string Telefone,
    string Endereco);
#endregion

#region ResultadoClienteDto
public record ClienteValidoDto(bool Valido, string Mensagem);
#endregion
#endregion