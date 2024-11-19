using Locadora.Aluguel.Enterprise;

namespace Locadora.Aluguel.Services.Integracoes;

#region Interface
public interface IClienteHelper
{
    Task<ResultadoClienteDto> ObterPorIdAsync(string codigoCliente);
}
#endregion

public class ClienteHelper : HttpClientBase, IClienteHelper
{
    private const string UrlBase = "https://localhost:7084/api";
    private const string ActionObterClientePorId = "/Clientes/{0}";

    public ClienteHelper(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<ResultadoClienteDto> ObterPorIdAsync(string codigoCliente)
    {
        var url = $"{UrlBase}{string.Format(ActionObterClientePorId, codigoCliente)}";
        var resposta = await GetAsync<ResultadoClienteDto>(url);

        if (resposta.Sucesso)
            return resposta.Conteudo;

        var mensagem = resposta?.Mensagem ?? "Sem resposta do serviço de cliente";
        throw new AluguelAppException(ETipoException.ErroIntegracaoCliente, mensagem);
    }
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