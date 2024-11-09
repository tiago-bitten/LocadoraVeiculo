using Locadora.Aluguel.Enterprise;

namespace Locadora.Aluguel.Models;

public sealed class Aluguel : EntidadeBase
{
    public Guid CodigoCliente { get; set; }
    public Guid CodigoVeiculo { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFinal { get; set; }
    public EStatusAluguel Status { get; private set; } = EStatusAluguel.EmAndamento;
    public decimal ValorTotal { get; set; }
    
    #region Regras
    public void ValidarDatas()
    {
        if (DataInicio > DataFinal)
            throw new AluguelAppException(ETipoException.DataInicialMaiorQueDataFinal);
    }
    
    public void Concluir() => Status = EStatusAluguel.Concluido;
    public void Programar() => Status = EStatusAluguel.Programdo;
    public void Cancelar() => Status = EStatusAluguel.Cancelado;
    #endregion
}

#region Enums
public enum EStatusAluguel {
    Programdo = 1,
    EmAndamento = 2,
    Concluido = 3,
    Cancelado = 4
}
#endregion