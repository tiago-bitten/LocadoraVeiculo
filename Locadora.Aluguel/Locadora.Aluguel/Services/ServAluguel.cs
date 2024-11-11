using Locadora.Aluguel.Enterprise;
using Locadora.Aluguel.Repositories;
using Locadora.Aluguel.Services.Infra;

namespace Locadora.Aluguel.Services;

#region Interface
public interface IServAluguel : IServBase<Models.Aluguel>
{
    Task ValidarDisponibilidade(Models.Aluguel aluguel, string idVeiculo, string idCliente);
}
#endregion

public class ServAluguel : ServBase<Models.Aluguel, IRepAluguel>, IServAluguel
{
    #region Ctor
    public ServAluguel(IRepAluguel repository) : base(repository)
    {
    }
    #endregion

    public async Task ValidarDisponibilidade(Models.Aluguel aluguel, string idVeiculo, string idCliente)
    {
        var clienteEstaComAluguelEmAndamento = await Repository.ClienteEstaComAluguelEmAndamentoAsync(idCliente);
        if (clienteEstaComAluguelEmAndamento)
            throw new AluguelAppException(ETipoException.ClienteEstaComAluguelEmAndamento);
        
        var veiculoEstaComAluguelEmAndamento = await Repository.VeiculoEstaComAluguelEmAndamentoAsync(idVeiculo);
        if (veiculoEstaComAluguelEmAndamento)
            throw new AluguelAppException(ETipoException.VeiculoEstaComAluguelEmAndamento);
    }
}