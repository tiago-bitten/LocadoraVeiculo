using Locadora.Aluguel.Enterprise;
using Locadora.Aluguel.Repositories;
using Locadora.Aluguel.Services.Infra;

namespace Locadora.Aluguel.Services;

#region Interface
public interface IServAluguel : IServBase<Models.Aluguel>
{
    Task ValidarDisponibilidade(Models.Aluguel aluguel, string codigoVeiculo, string codigoCliente);
}
#endregion

public class ServAluguel : ServBase<Models.Aluguel, IRepAluguel>, IServAluguel
{
    #region Ctor
    public ServAluguel(IRepAluguel repository) : base(repository)
    {
    }
    #endregion

    public async Task ValidarDisponibilidade(Models.Aluguel aluguel, string codigoVeiculo, string codigoCliente)
    {
        var clienteEstaComAluguelEmAndamento = await Repository.ClienteEstaComAluguelEmAndamentoAsync(codigoCliente);
        if (clienteEstaComAluguelEmAndamento)
            throw new AluguelAppException(ETipoException.ClienteEstaComAluguelEmAndamento);
        
        var veiculoEstaComAluguelEmAndamento = await Repository.VeiculoEstaComAluguelEmAndamentoAsync(codigoVeiculo);
        if (veiculoEstaComAluguelEmAndamento)
            throw new AluguelAppException(ETipoException.VeiculoEstaComAluguelEmAndamento);
    }
}