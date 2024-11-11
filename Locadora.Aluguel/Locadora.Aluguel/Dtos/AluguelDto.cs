using AutoMapper;

namespace Locadora.Aluguel.Dtos;

public class AluguelProfile : Profile
{
    public AluguelProfile()
    {
        CreateMap<AdicionarAluguelDto, Models.Aluguel>()
            .ForMember(dest => dest.ValorTotal, 
                opt => opt.MapFrom(x => x.ValorDiaria * x.TotalDias));
        CreateMap<Models.Aluguel, RespostaAluguelDto>();
    }
}

public record AdicionarAluguelDto(
    string CodigoCliente,
    string CodigoVeiculo,
    decimal ValorDiaria,
    DateTime DataInicial,
    DateTime DataFinal)
{
    public int TotalDias => (DataInicial - DataFinal).Days;
}
    
public record RespostaAluguelDto(
    string Id,
    string CodigoCliente,
    string CodigoVeiculo,
    decimal ValorTotal,
    DateTime DataInicial,
    DateTime DataFinal);