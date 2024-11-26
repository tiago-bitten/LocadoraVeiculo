using Locadora.Veiculo.Enterprise;
using Locadora.Veiculo.Models;
using Locadora.Veiculo.Repositories;
using Locadora.Veiculo.Services.Infra;

namespace Locadora.Veiculo.Services;

#region Inteface
public interface IServManutencao : IServBase<Manutencao>
{
    void Concluir(Manutencao manutencao);
    void Cancelar(Manutencao manutencao);
    void IniciarProgramada(Manutencao manutencao);
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
        if (manutencao.Status == EStatusManutencao.Programado)
        {
            var possuiManutencaoProgramada = await Repository.PossuiManutencaoProgramadaPorVeiculoAsync(manutencao.CodigoVeiculo,
                manutencao.DataInicio, manutencao.DataFinal);

            if (possuiManutencaoProgramada)
                throw new VeiculoAppException(ETipoException.VeiculoJaPossuiManutencaoProgramada);
        }
        
        await base.AdicionarAsync(manutencao);
    }

    #endregion
    
    #region Concluir
    public void Concluir(Manutencao manutencao)
    {
        manutencao.ValidarManutencaoConcluida();
        
        manutencao.Concluir();
        
        Atualizar(manutencao);
    }
    #endregion
    
    #region Cancelar
    public void Cancelar(Manutencao manutencao)
    {
        if (manutencao.Status is not (EStatusManutencao.EmAndamento or EStatusManutencao.Programado))
            throw new VeiculoAppException(ETipoException.ManutencaoNaoPodeSerCancelada);
        
        manutencao.Cancelar();
        Atualizar(manutencao);
    }
    #endregion
    
    #region IniciarProgramada
    public void IniciarProgramada(Manutencao manutencao)
    {
        if (manutencao.Status is not EStatusManutencao.Programado)
            throw new VeiculoAppException(ETipoException.ManutencaoNaoPodeSerIniciada);

        manutencao.EmAndamento();
        manutencao.DataInicio = DateTime.Now;
        if (manutencao.DataFinal.HasValue)
        {
            var subtrairDias = (int)manutencao.DataInicio.Subtract(DateTime.Now).TotalDays;
            manutencao.DataFinal = manutencao.DataFinal.Value.AddDays(subtrairDias);
        }
        Atualizar(manutencao);
    }
    #endregion    
}