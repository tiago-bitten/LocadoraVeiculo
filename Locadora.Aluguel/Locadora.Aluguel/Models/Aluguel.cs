using System.Text.Json.Serialization;
using Locadora.Aluguel.Enterprise;

namespace Locadora.Aluguel.Models;

public sealed class Aluguel : EntidadeBase
{
    public string CodigoCliente { get; set; }
    public string CodigoVeiculo { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFinal { get; set; }
    public EStatusAluguel Status { get; private set; } = EStatusAluguel.EmAndamento;
    public decimal ValorTotal { get; set; }
    
    #region Regras
    
    #region ValidarDatas
    public void ValidarDatas()
    {
        if (DataInicio > DataFinal)
            throw new AluguelAppException(ETipoException.DataInicialMaiorQueDataFinal);
    }
    #endregion

    #region ValidarValorTotal
    public void ValidarValorTotal()
    {
        if (ValorTotal <= 0)
            throw new AluguelAppException(ETipoException.ValorTotalAluguelInvalido);
    }
    #endregion
    
    #region Status
    public void Programar() => Status = EStatusAluguel.Programdo;
    public void EmAndamento() => Status = EStatusAluguel.EmAndamento;
    public void Concluir() => Status = EStatusAluguel.Concluido;
    public void Cancelar() => Status = EStatusAluguel.Cancelado;
    #endregion
    #endregion
}

#region Enums
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EStatusAluguel {
    Programdo = 1,
    EmAndamento = 2,
    Concluido = 3,
    Cancelado = 4
}
#endregion