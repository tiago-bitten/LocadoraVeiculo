using Locadora.Aluguel.Enterprise;
using Locadora.Aluguel.Repositories;
using Locadora.Aluguel.Services.Infra;
using Locadora.Aluguel.Services.Integracoes;

namespace Locadora.Aluguel.Services;

#region Interface
public interface IServAluguel : IServBase<Models.Aluguel>
{
    Task ValidarDisponibilidadeAsync(Models.Aluguel aluguel);
    Task ValidarDisponibilidadeClienteAsync(string codigoCliente);
    Task ValidarDisponibilidadeVeiculoAsync(string codigoVeiculo);
}
#endregion

public class ServAluguel : ServBase<Models.Aluguel, IRepAluguel>, IServAluguel
{
    #region Ctor
    private readonly IClienteHelper _clienteHelper;
    private readonly IVeiculoHelper _veiculoHelper;
    
    public ServAluguel(IRepAluguel repository,
                       IClienteHelper clienteHelper,
                       IVeiculoHelper veiculoHelper) 
        : base(repository)
    {
        _clienteHelper = clienteHelper;
        _veiculoHelper = veiculoHelper;
    }
    #endregion
    
    #region AdicionarAsync
    public override async Task AdicionarAsync(Models.Aluguel aluguel)
    {
        aluguel.ValidarValorTotal();
        await ValidarDisponibilidadeAsync(aluguel);
        await ValidarDisponibilidadeClienteAsync(aluguel.CodigoCliente);
        await ValidarDisponibilidadeVeiculoAsync(aluguel.CodigoVeiculo);

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
    
    #region ValidarDisponibilidadeClienteAsync
    public async Task ValidarDisponibilidadeClienteAsync(string codigoCliente)
    {
        var valido = await _clienteHelper.ValidarClienteAsync(codigoCliente);
        
        if (!valido.Valido)
            throw new AluguelAppException(ETipoException.ClienteNaoValido, valido.Mensagem);
    }
    #endregion
    
    #region ValidarDisponibilidadeVeiculoAsync
    public async Task ValidarDisponibilidadeVeiculoAsync(string codigoVeiculo)
    {
        var valido = await _veiculoHelper.ValidarVeiculoAsync(codigoVeiculo);
        
        if (!valido.Valido)
            throw new AluguelAppException(ETipoException.VeiculoNaoValido, valido.Mensagem);
    }
    #endregion
}