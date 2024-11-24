using Hangfire;
using Locadora.Aluguel.AplicServices;
using Locadora.Aluguel.Dtos;
using Locadora.Aluguel.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Aluguel.Services.Jobs;

#region Interface
public interface IAluguelJob
{
    Task IniciarAlugueisProgramadosAsync();
    [Queue("alugueis")]
    Task IniciarAluguelProgramadoAsync(IniciarAluguelProgramadoDto aluguel);
}
#endregion

public class AluguelJob : IAluguelJob
{
    #region Ctor
    private readonly IAplicAluguel _aplicAluguel;
    private readonly IRepAluguel _repAluguel;

    public AluguelJob(IAplicAluguel aplicAluguel,
                      IRepAluguel repAluguel)
    {
        _aplicAluguel = aplicAluguel;
        _repAluguel = repAluguel;
    }
    #endregion

    #region IniciarAlugueisProgramadosAsync
    public async Task IniciarAlugueisProgramadosAsync()
    {
        var alugueisProgramados = await _repAluguel.ObterAlugueisProgramados()
            .Where(x => x.DataInicio.Date == DateTime.Now.Date)
            .Select(x => new
            {
                x.Id,
                x.DataInicio
            })
            .ToListAsync();

        foreach (var aluguel in alugueisProgramados)
        {
            var dto = new IniciarAluguelProgramadoDto(aluguel.Id);
            var delay = aluguel.DataInicio - DateTime.Now;

            if (delay > TimeSpan.Zero)
            {
                BackgroundJob.Schedule<IAluguelJob>(x => x.IniciarAluguelProgramadoAsync(dto), delay);
            }
            else
            {
                BackgroundJob.Enqueue<IAluguelJob>(x => x.IniciarAluguelProgramadoAsync(dto));
            }
        }
    }

    [Queue("alugueis")]
    public Task IniciarAluguelProgramadoAsync(IniciarAluguelProgramadoDto aluguel)
    {
        return _aplicAluguel.IniciarProgramadoAsync(aluguel);
    }
    #endregion
}