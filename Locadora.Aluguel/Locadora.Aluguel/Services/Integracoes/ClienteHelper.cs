using Locadora.Aluguel.Controllers.Infra;
using Locadora.Aluguel.Enterprise;
using Locadora.Aluguel.Extensions;

namespace Locadora.Aluguel.Services.Integracoes;

#region Interface
public interface IClienteHelper
{
    Task<ResultadoClienteDto> ObterPorIdAsync(string codigoCliente);
}
#endregion

public class ClienteHelper : IClienteHelper
{
    #region Constants
    private string UrlBase => "https://localhost:7084/api";

    private string ActionObterClientePorId => "/Clientes/{0}";
    #endregion
    
    #region Ctor
    private readonly HttpClient _httpClient;

    public ClienteHelper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    #endregion
    
    #region ObterPorIdAsync
    public async Task<ResultadoClienteDto> ObterPorIdAsync(string codigoCliente)
    {
        var url = UrlBase + string.Format(ActionObterClientePorId, codigoCliente);

        var resposta = await _httpClient.GetAsync<ResultadoClienteDto>(url);

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
#endregion