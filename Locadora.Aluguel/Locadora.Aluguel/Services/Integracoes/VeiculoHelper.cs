using Locadora.Aluguel.Enterprise;

namespace Locadora.Aluguel.Services.Integracoes;

#region Interface
public interface IVeiculoHelper
{
    Task<ResultadoVeiculoDto> ObterPorIdAsync(string codigoCliente);
}
#endregion

public class VeiculoHelper : HttpClientBase, IVeiculoHelper
{
    private const string UrlBase = "https://localhost:6001/api";
    private const string ActionObterVeiculoPorId = "/Veiculos/{0}";

    public VeiculoHelper(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<ResultadoVeiculoDto> ObterPorIdAsync(string codigoVeiculo)
    {
        var url = $"{UrlBase}{string.Format(ActionObterVeiculoPorId, codigoVeiculo)}";
        var resposta = await GetAsync<ResultadoVeiculoDto>(url);

        if (resposta.Sucesso)
            return resposta.Conteudo;

        var mensagem = resposta?.Mensagem ?? "Sem resposta do serviço de cliente";
        throw new AluguelAppException(ETipoException.ErroIntegracaoCliente, mensagem);
    }
}

#region Dtos

#region ResultadoVeiculoDto
public record ResultadoVeiculoDto(
    string Id,
    string Marca,
    string Modelo,
    string Tipo,
    DateTime DataFabricacao,
    string placa,
    string Status,
    decimal ValorDiaria);

#endregion
#endregion