using Locadora.Veiculo.Models;
using Locadora.Veiculo.Repositories.Context;
using Locadora.Veiculo.Repositories.Infra;

namespace Locadora.Veiculo.Repositories;

#region Interface
public interface IRepVeiculo : IRepBase<Models.Veiculo>
{
    Task<Models.Veiculo?> ObterPorPlacaAsync(string placa);
    IQueryable<Models.Veiculo> ObterParaAlugar(DateTime dataInicial, DateTime dataFinal);
}
#endregion
    
public class RepVeiculo : RepBase<Models.Veiculo>, IRepVeiculo
{
    #region Ctor
    public RepVeiculo(VeiculoDbContext context) : base(context)
    {
    }
    #endregion

    #region ObterPorPlacaAsync
    public Task<Models.Veiculo?> ObterPorPlacaAsync(string placa)
    {
        return BuscarAsync(x => x.Placa == placa);
    }
    #endregion

    #region ObterParaAlugar
    public IQueryable<Models.Veiculo> ObterParaAlugar(DateTime dataInicial, DateTime dataFinal)
    {
        var query = from ve in DbSet
            join ma in Context.Set<Manutencao>() on ve.Id equals ma.CodigoVeiculo into manutencoes
            from ma in manutencoes.DefaultIfEmpty()
            where ve.Status == EStatusVeiculo.Disponivel
                  && (ma is null 
                      || (ma.Status != EStatusManutencao.EmAndamento 
                          && !(ma.Status == EStatusManutencao.Programada && ma.DataInicio <= dataFinal && ma.DataFinal >= dataInicial))
                  )
            select ve;

        return query;
    }

    #endregion
}