using Locadora.Veiculo.Enterprise;
using Locadora.Veiculo.Models;
using Locadora.Veiculo.Repositories;
using Locadora.Veiculo.Services.Infra;

namespace Locadora.Veiculo.Services;

#region Interface
public interface IServVeiculo : IServBase<Models.Veiculo>
{
    IQueryable<Models.Veiculo> ObterParaAlugar(DateTime dataInicial, DateTime dataFinal);
    Task<(bool Valido, string Mensagem)> ValidoParaAlugarAsync(Models.Veiculo veiculo, DateTime dataInicial, DateTime dataFinal);
}
#endregion

public class ServVeiculo : ServBase<Models.Veiculo, IRepVeiculo>, IServVeiculo
{
    #region Ctor

    private readonly IRepManutencao _repManutencao;
    
    public ServVeiculo(IRepVeiculo repository, 
                       IRepManutencao repManutencao)
        : base(repository)
    {
        _repManutencao = repManutencao;
    }
    #endregion

    #region AdicionarAsync
    public override async Task AdicionarAsync(Models.Veiculo veiculo)
    {
        var existePorPlaca = await Repository.ObterPorPlacaAsync(veiculo.Placa);
        if (existePorPlaca is not null)
            throw new VeiculoAppException(ETipoException.VeiculoComPlacaJaExiste);

        veiculo.ValidarCompatibilidadeTipoModelo();
        veiculo.ValidarValorDiaria();

        await base.AdicionarAsync(veiculo);
    }
    #endregion

    #region ObterParaAlugar
    public IQueryable<Models.Veiculo> ObterParaAlugar(DateTime dataInicial, DateTime dataFinal)
    {
        return Repository.ObterParaAlugar(dataInicial, dataFinal);
    }
    #endregion
    
    #region ValidoParaAlugar
    public async Task<(bool Valido, string Mensagem)> ValidoParaAlugarAsync(Models.Veiculo veiculo, DateTime dataInicial, DateTime dataFinal)
    {
        if (veiculo.Inativo || veiculo.Status is not EStatusVeiculo.Disponivel)
            return (false, "Veículo não está disponível.");

        var manutencaoProgramada = await _repManutencao.PossuiManutencaoProgramadaPorVeiculoAsync(veiculo.Id, dataInicial, dataFinal);
        if (manutencaoProgramada)
            return (false, "Veículo possui manutenção programada.");
        
        return (true, "Veículo está disponível.");
    }
    #endregion
}