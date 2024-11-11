using Locadora.Aluguel.Enterprise;
using Locadora.Aluguel.Repositories;
using Locadora.Aluguel.Services.Infra;

namespace Locadora.Aluguel.Services;

#region Interface
public interface IServAluguel : IServBase<Models.Aluguel>
{
    Task ValidarDisponibilidadeAsync(Models.Aluguel aluguel);
}
#endregion

public class ServAluguel : ServBase<Models.Aluguel, IRepAluguel>, IServAluguel
{
    #region Ctor
    public ServAluguel(IRepAluguel repository) : base(repository)
    {
    }
    #endregion
    
    #region AdicionarAsync
    public override async Task AdicionarAsync(Models.Aluguel aluguel)
    {
        aluguel.ValidarValorTotal();
        await ValidarDisponibilidadeAsync(aluguel);

        await base.AdicionarAsync(aluguel);
    }

    #endregion

    #region ValidarDisponibilidadeAsync
    public async Task ValidarDisponibilidadeAsync(Models.Aluguel aluguel)
    {
        var clienteEstaComAluguelEmAndamento = await Repository.ClienteEstaComAluguelEmAndamentoAsync(aluguel.CodigoCliente);
        if (clienteEstaComAluguelEmAndamento)
            throw new AluguelAppException(ETipoException.ClienteEstaComAluguelEmAndamento);
        
        var veiculoEstaComAluguelEmAndamento = await Repository.VeiculoEstaComAluguelEmAndamentoAsync(aluguel.CodigoVeiculo);
        if (veiculoEstaComAluguelEmAndamento)
            throw new AluguelAppException(ETipoException.VeiculoEstaComAluguelEmAndamento);
    }
    #endregion
}