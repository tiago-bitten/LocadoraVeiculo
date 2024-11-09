using Locadora.Veiculo.Enterprise;
using Locadora.Veiculo.Repositories;
using Locadora.Veiculo.Services.Infra;

namespace Locadora.Veiculo.Services;

#region Interface
public interface IServVeiculo : IServBase<Models.Veiculo>
{
    IQueryable<Models.Veiculo> ObterParaAlugar(DateTime dataInicial, DateTime dataFinal);
}
#endregion

public class ServVeiculo : ServBase<Models.Veiculo, IRepVeiculo>, IServVeiculo
{
    #region Ctor
    public ServVeiculo(IRepVeiculo repository) : base(repository)
    {
    }
    #endregion

    #region AdicionarAsync
    public override async Task AdicionarAsync(Models.Veiculo veiculo)
    {
        var existePorPlaca = await Repository.ObterPorPlacaAsync(veiculo.Placa);
        if (existePorPlaca is not null)
            throw new VeiculoAppException(ETipoException.VeiculoComPlacaJaExiste);

        veiculo.ValidarCompatibilidadeTipoModelo();

        await base.AdicionarAsync(veiculo);
    }
    #endregion

    #region ObterParaAlugar
    public IQueryable<Models.Veiculo> ObterParaAlugar(DateTime dataInicial, DateTime dataFinal)
    {
        return Repository.ObterParaAlugar(dataInicial, dataFinal);
    }
    #endregion
}