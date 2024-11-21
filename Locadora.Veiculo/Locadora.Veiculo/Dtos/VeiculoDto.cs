using AutoMapper;
using Locadora.Veiculo.Enterprise;
using Locadora.Veiculo.Models;

namespace Locadora.Veiculo.Dtos;

#region QueryObterParaAlugar
public class QueryObterParaAlugar : QueryFiltro
{
    public DateTime DataInicial { get; set; }
    public DateTime DataFinal { get; set; }
}
#endregion

#region QueryValidarParaAlugar
public record QueryValidarParaAlugar(
    string CodigoVeiculo,
    DateTime DataInicial,
    DateTime DataFinal);
#endregion

public class VeiculoProfile : Profile
{
    public VeiculoProfile()
    {
        CreateMap<AdicionarVeiculoDto, Models.Veiculo>();
        CreateMap<Models.Veiculo, ResultadoVeiculoDto>();
    }
}

#region AdicionarVeiculoDto
public record AdicionarVeiculoDto(
    string Marca,
    EModeloVeiculo Modelo,
    ETipoVeiculo Tipo,
    DateTime DataFabricacao,
    string Placa,
    decimal ValorDiaria);
#endregion 

#region ResultadoVeiculoDto
public record ResultadoVeiculoDto(
    string Id,
    string Marca,
    EModeloVeiculo Modelo,
    ETipoVeiculo Tipo,
    DateTime DataFabricacao,
    string Placa,
    EStatusVeiculo Status,
    decimal ValorDiaria);
#endregion

#region AtualizarVeiculoDto
public record AtualizarVeiculoDto(
    string CodigoVeiculo,
    string Marca,
    EModeloVeiculo Modelo,
    ETipoVeiculo Tipo,
    DateTime DataFabricacao,
    string Placa,
    decimal ValorDiaria);
#endregion

#region VeiculoValidoDto
public record VeiculoValidoDto(bool Valido, string Mensagem);
#endregion