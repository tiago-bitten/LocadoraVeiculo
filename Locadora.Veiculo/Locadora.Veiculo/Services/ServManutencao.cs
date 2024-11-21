using Locadora.Veiculo.Enterprise;
using Locadora.Veiculo.Models;
using Locadora.Veiculo.Repositories;
using Locadora.Veiculo.Services.Infra;

namespace Locadora.Veiculo.Services;

#region Inteface
public interface IServManutencao : IServBase<Manutencao>
{
}
#endregion

public class ServManutencao : ServBase<Manutencao, IRepManutencao>, IServManutencao
{
    #region Ctor
    public ServManutencao(IRepManutencao repository) : base(repository)
    {
    }
    #endregion
    
    #region AdicionarAsync

    public override async Task AdicionarAsync(Manutencao manutencao)
    {
        if (manutencao.Status == EStatusManutencao.Programada)
        {
            var possuiManutencaoProgramada = await Repository.PossuiManutencaoProgramadaPorVeiculoAsync(manutencao.CodigoVeiculo,
                manutencao.DataInicio, manutencao.DataFinal);

            if (possuiManutencaoProgramada)
                throw new VeiculoAppException(ETipoException.VeiculoJaPossuiManutencaoProgramada);
        }
        
        await base.AdicionarAsync(manutencao);
    }

    #endregion
}