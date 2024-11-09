using Locadora.Aluguel.Enterprise;

namespace Locadora.Aluguel.Models;

public sealed class Aluguel : EntidadeBase
{
    public Guid CodigoCliente { get; set; }
    public Guid CodigoVeiculo { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFinal { get; set; }
    public EStatusAluguel Status { get; private set; } = EStatusAluguel.EmAndamento;
    
    #region Regras
    public void ValidarDatas()
    {
        if (DataInicio > DataFinal)
            throw new AluguelAppException(ETipoException.DataInicialMaiorQueDataFinal);
    }
    
    public void Concluir() => Status = EStatusAluguel.Concluido;
    #endregion
}

#region Enums
public enum EStatusAluguel {
    EmAndamento = 1,
    Concluido = 2
}
#endregion