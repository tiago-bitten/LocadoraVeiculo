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
    DateTime DataInicio,
    DateTime DataFinal)
{
    public decimal TotalDias => (decimal)DataFinal.Subtract(DataInicio).TotalMinutes / 1440;
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

#region CancelarAluguelDto
public record CancelarAluguelDto(string Id);
#endregion

#region IniciarAluguelProgramadoDto
public record IniciarAluguelProgramadoDto(string CodigoAluguel);
#endregion