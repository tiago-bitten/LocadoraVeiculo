using Locadora.Veiculo.Models;
using Locadora.Veiculo.Repositories.Context;
using Locadora.Veiculo.Repositories.Infra;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Veiculo.Repositories;

#region Interfaces

public interface IRepManutencao : IRepBase<Manutencao>
{
    IQueryable<Manutencao> ObterPorVeiculo(string codigoVeiculo);
    Task<bool> PossuiManutencaoProgramadaPorVeiculoAsync(string codigoVeiculo, DateTime dataInicial, DateTime dataFinal);
}
#endregion

public class RepManutencao : RepBase<Manutencao>, IRepManutencao
{
    #region Ctor
    public RepManutencao(VeiculoDbContext context)
        : base(context)
    {
    }
    #endregion

    #region ObterPorVeiculo
    public IQueryable<Manutencao> ObterPorVeiculo(string codigoVeiculo)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region PossuiManutencaoProgramadaPorVeiculoAsync
    public Task<bool> PossuiManutencaoProgramadaPorVeiculoAsync(string codigoVeiculo, DateTime dataInicial, DateTime dataFinal)
    {
        return (from ma in DbSet
            where ma.CodigoVeiculo == codigoVeiculo
                  && ma.Status == EStatusManutencao.Programada
                    && ma.DataInicio <= dataFinal && ma.DataFinal >= dataInicial
            select 1).AnyAsync();
    }
    #endregion
}