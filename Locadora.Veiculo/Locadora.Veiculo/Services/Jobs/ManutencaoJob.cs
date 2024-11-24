using Hangfire;
using Locadora.Veiculo.AplicServices;
using Locadora.Veiculo.Dtos;
using Locadora.Veiculo.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Veiculo.Services.Jobs;

#region Interface
public interface IManutencaoJob
{
    Task IniciarManutencoesProgramadasAsync();
    [Queue("manutencoes")]
    Task IniciarManutencaoProgramadaAsync(IniciarManutencaoProgramadaDto dto);
}
#endregion

public class ManutencaoJob : IManutencaoJob
{
    #region Ctor
    private readonly IAplicManutencao _aplicManutencao;
    private readonly IRepManutencao _repManutencao;

    public ManutencaoJob(IAplicManutencao aplicManutencao,
                         IRepManutencao repManutencao)
    {
        _aplicManutencao = aplicManutencao;
        _repManutencao = repManutencao;
    }
    #endregion
    
    #region IniciarManutencoesProgramadasAsync
    public async Task IniciarManutencoesProgramadasAsync()
    {
        var manutencoes = await _repManutencao.ObterManutencoesProgramadas()
            .Where(x => x.DataInicio.Date == DateTime.Today)
            .Select(x => new IniciarManutencaoProgramadaDto(x.Id))
            .ToListAsync();

        foreach (var manutencao in manutencoes)
        {
            BackgroundJob.Enqueue<IManutencaoJob>(x => x.IniciarManutencaoProgramadaAsync(manutencao));
        }
    }

    #region IniciarManutencaoAsync
    [Queue("manutencoes")]
    public Task IniciarManutencaoProgramadaAsync(IniciarManutencaoProgramadaDto dto)
    {
        return _aplicManutencao.IniciaroProgramadaAsync(dto);
    }
    #endregion
    #endregion
}