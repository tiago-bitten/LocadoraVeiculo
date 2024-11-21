using Locadora.Aluguel.Enterprise;
using Locadora.Aluguel.Models;
using Locadora.Aluguel.Repositories;
using Locadora.Aluguel.Services.Infra;
using Locadora.Aluguel.Services.Integracoes;

namespace Locadora.Aluguel.Services;

#region Interface
public interface IServAluguel : IServBase<Models.Aluguel>
{
    Task ValidarDisponibilidadeAsync(Models.Aluguel aluguel);
    Task ValidarDisponibilidadeClienteAsync(string codigoCliente);
    Task ValidarDisponibilidadeVeiculoAsync(Models.Aluguel aluguel);
    void Concluir(Models.Aluguel aluguel);
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
        await ValidarDisponibilidadeVeiculoAsync(aluguel);

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
    public async Task ValidarDisponibilidadeVeiculoAsync(Models.Aluguel aluguel)
    {
        var valido = await _veiculoHelper.ValidarVeiculoAsync(new QueryValidarParaAlugar(aluguel.CodigoVeiculo, aluguel.DataInicio, aluguel.DataFinal));
        
        if (!valido.Valido)
            throw new AluguelAppException(ETipoException.VeiculoNaoValido, valido.Mensagem);
    }
    #endregion
    
    #region ConcluirAsync
    public void Concluir(Models.Aluguel aluguel)
    {
        if (aluguel.Status is not EStatusAluguel.EmAndamento)
            throw new AluguelAppException(ETipoException.AluguelNaoPodeSerConcluido);
        
        aluguel.Concluir();
        Atualizar(aluguel);
    }
    #endregion
}