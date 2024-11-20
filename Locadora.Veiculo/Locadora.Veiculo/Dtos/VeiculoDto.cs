using AutoMapper;
using Locadora.Veiculo.Enterprise;
using Locadora.Veiculo.Models;

namespace Locadora.Veiculo.Dtos;

public class QueryObterParaAlugar : QueryFiltro
{
    public DateTime DataInicial { get; set; }
    public DateTime DataFinal { get; set; }
}

public record QueryValidarParaAlugar(
    string Id,
    DateTime DataInicial,
    DateTime DataFinal);

public class VeiculoProfile : Profile
{
    public VeiculoProfile()
    {
        CreateMap<AdicionarVeiculoDto, Models.Veiculo>();
        CreateMap<Models.Veiculo, ResultadoVeiculoDto>();
    }
}

public record AdicionarVeiculoDto(
    string Marca,
    EModeloVeiculo Modelo,
    ETipoVeiculo Tipo,
    DateTime DataFabricacao,
    string placa,
    decimal ValorDiaria);

public record ResultadoVeiculoDto(
    string Id,
    string Marca,
    EModeloVeiculo Modelo,
    ETipoVeiculo Tipo,
    DateTime DataFabricacao,
    string placa,
    EStatusVeiculo Status,
    decimal ValorDiaria);

public record VeiculoValidoDto(bool Valido, string Mensagem);
    