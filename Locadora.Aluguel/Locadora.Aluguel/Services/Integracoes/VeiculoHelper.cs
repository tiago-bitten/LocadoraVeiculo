using Locadora.Aluguel.Enterprise;

namespace Locadora.Aluguel.Services.Integracoes;

#region Interface
public interface IVeiculoHelper
{
    Task<ResultadoVeiculoDto> ObterPorIdAsync(string codigoCliente);
    Task<VeiculoValidoDto> ValidarVeiculoAsync(QueryValidarParaAlugar query);
    Task DefinirStatusAsync(string codigoVeiculo, string status);
}
#endregion

public class VeiculoHelper : HttpClientBase, IVeiculoHelper
{
    #region Ctor
    private const string UrlBase = "https://localhost:60537/api";
    private const string ActionObterVeiculoPorId = "/Veiculos/{0}";
    private const string ActionValidarParaAlugar = "/Veiculos/ValidarParaAlugar?{0}";
    private const string ActionDefinirStatus = "/Veiculos/DefinirStatus";

    public VeiculoHelper(HttpClient httpClient) : base(httpClient)
    {
    }
    #endregion

    #region ObterPorIdAsync
    public async Task<ResultadoVeiculoDto> ObterPorIdAsync(string codigoVeiculo)
    {
        var url = $"{UrlBase}{string.Format(ActionObterVeiculoPorId, codigoVeiculo)}";
        var resposta = await GetAsync<ResultadoVeiculoDto>(url);

        if (resposta.Sucesso)
            return resposta.Conteudo;

        var mensagem = resposta?.Mensagem ?? "Sem resposta do serviço de cliente";
        throw new AluguelAppException(ETipoException.ErroIntegracaoCliente, mensagem);
    }
    #endregion
    
    #region ValidarVeiculoAsync
    public async Task<VeiculoValidoDto> ValidarVeiculoAsync(QueryValidarParaAlugar query)
    {
        var queryStr = $"CodigoVeiculo={query.CodigoVeiculo}&DataInicial={query.DataInicial:yyyy-MM-dd}&DataFinal={query.DataFinal:yyyy-MM-dd}";
        var url = $"{UrlBase}{string.Format(ActionValidarParaAlugar, queryStr)}";
        var resposta = await GetAsync<VeiculoValidoDto>(url);

        if (resposta.Sucesso)
            return resposta.Conteudo;

        var mensagem = resposta?.Mensagem ?? "Sem resposta do serviço de veículo";
        throw new AluguelAppException(ETipoException.ErroIntegracaoVeiculo, mensagem);
    }
    #endregion

    #region DefinirStatusAsync
    public async Task DefinirStatusAsync(string codigoVeiculo, string status)
    {
        var url = $"{UrlBase}{ActionDefinirStatus}";
        var json = new { CodigoVeiculo = codigoVeiculo, Status = status };
        var resposta = await PutAsync<dynamic>(url, json);

        if (resposta.Sucesso)
            return;

        var mensagem = resposta?.Mensagem ?? "Sem resposta do serviço de veículo";
        throw new AluguelAppException(ETipoException.ErroIntegracaoVeiculo, mensagem);
    }
    #endregion
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

#region VeiculoValidoDto
public record VeiculoValidoDto(
    bool Valido,
    string Mensagem);
#endregion

#region QueryValidarParaAlugar
public record QueryValidarParaAlugar(
    string CodigoVeiculo,
    DateTime DataInicial,
    DateTime DataFinal);
#endregion
#endregion