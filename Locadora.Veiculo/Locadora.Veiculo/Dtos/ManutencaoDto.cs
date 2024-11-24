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

#region CriarManutencaoDto
public record CriarManutencaoDto(
    string CodigoVeiculo,
    ETipoManutencao Tipo,
    DateTime DataInicio,
    DateTime? DataFinal);
#endregion

#region ResultadoManutencaoDto
public record ResultadoManutencaoDto(
    string Id,
    string CodigoVeiculo,
    ETipoManutencao Tipo,
    DateTime DataInicio,
    DateTime? DataFinal,
    EStatusManutencao Status);
#endregion
    
#region CancelarManutencaoDto
public record CancelarManutencaoDto(string Id);
#endregion

#region ConcluirManutencaoDto
public record ConcluirManutencaoDto(string Id);
#endregion

#region IniciarManutencaoProgramadaDto
public record IniciarManutencaoProgramadaDto(string CodigoManutencao);
#endregion