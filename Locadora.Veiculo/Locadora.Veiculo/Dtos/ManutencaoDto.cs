using AutoMapper;
using Locadora.Veiculo.Models;

namespace Locadora.Veiculo.Dtos;

public class ManutencaoProfile : Profile
{
    public ManutencaoProfile()
    {
        CreateMap<CriarManutencaoDto, Manutencao>();
        CreateMap<Manutencao, ResultadoManutencaoDto>();
    }
}

public record CriarManutencaoDto(
    string CodigoVeiculo,
    ETipoManutencao Tipo,
    DateTime DataInicio,
    DateTime? DataFinal);
    
public record ResultadoManutencaoDto(
    string Id,
    string CodigoVeiculo,
    ETipoManutencao Tipo,
    DateTime DataInicio,
    DateTime? DataFinal,
    EStatusManutencao Status); 