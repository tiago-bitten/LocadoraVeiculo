using AutoMapper;
using Locadora.Aluguel.Models;

namespace Locadora.Aluguel.Dtos;

public class AluguelProfile : Profile
{
    public AluguelProfile()
    {
        CreateMap<AdicionarAluguelDto, Models.Aluguel>();
        CreateMap<Models.Aluguel, RespostaAluguelDto>();
    }
}

#region AdicionarAluguelDto
public record AdicionarAluguelDto(
    string CodigoCliente,
    string CodigoVeiculo,
    decimal ValorDiaria,
    DateTime DataInicio,
    DateTime DataFinal)
{
    public int TotalDias => (DataInicio - DataFinal).Days;
    public decimal ValorTotal => ValorDiaria * TotalDias;
}
#endregion 

#region RespostaAluguelDto
public record RespostaAluguelDto(
    string Id,
    string CodigoCliente,
    string CodigoVeiculo,
    decimal ValorTotal,
    DateTime DataInicio,
    DateTime DataFinal,
    EStatusAluguel Status);
#endregion

#region ConcluirAluguelDto
public record ConcluirAluguelDto(string Id);
#endregion