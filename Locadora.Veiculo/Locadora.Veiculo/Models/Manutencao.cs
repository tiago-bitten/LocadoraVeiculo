using Locadora.Veiculo.Enterprise;

namespace Locadora.Veiculo.Models;

public sealed class Manutencao : EntidadeBase
{
    public string CodigoVeiculo { get; set; }
    public ETipoManutencao Tipo { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFinal { get; set; }
    public EStatusManutencao Status { get; private set; } = EStatusManutencao.Pendente;

    //
    
    public Veiculo Veiculo { get; set; }
    
    #region Regras
    public void ValidarDatas()
    {
        if (!DataFinal.HasValue)
            return;
        
        if (DataInicio > DataFinal.Value)
            throw new VeiculoAppException(ETipoException.DataInicialMaiorQueFinal);
    }

    public void ValidarManutencaoConcluida()
    {
        if (Status == EStatusManutencao.Concluido && !DataFinal.HasValue)
            throw new VeiculoAppException(ETipoException.ManutencaoConcluidaSemDataFinal);
    }

    #region Status
    public void Programar() => Status = EStatusManutencao.Programado;
    public void EmAndamento() => Status = EStatusManutencao.EmAndamento;
    public void Concluir() => Status = EStatusManutencao.Concluido;
    public void Cancelar() => Status = EStatusManutencao.Cancelado;
    #endregion
    #endregion
}

#region Enums
public enum ETipoManutencao
{
    Preventiva = 1,
    Corretiva = 2
}

public enum EStatusManutencao
{
    Programado = 1,
    Pendente = 2,
    EmAndamento = 3,
    Concluido = 5,
    Cancelado = 6
}
#endregion