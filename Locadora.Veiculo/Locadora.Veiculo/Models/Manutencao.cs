﻿using Locadora.Veiculo.Enterprise;

namespace Locadora.Veiculo.Models;

public class Manutencao : EntidadeBase
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
        if (DataInicio > DataFinal)
            throw new VeiculoAppException(ETipoException.DataInicialMaiorQueFinal);
    }

    public void ValidarManutencaoConcluida()
    {
        if (Status == EStatusManutencao.Concluida && !DataFinal.HasValue)
            throw new VeiculoAppException(ETipoException.ManutencaoConcluidaSemDataFinal);
    }

    #region Status
    public void Programar() => Status = EStatusManutencao.Programada;
    public void EmAndamento() => Status = EStatusManutencao.EmAndamento;
    public void Concluir() => Status = EStatusManutencao.Concluida;
    public void Cancelar() => Status = EStatusManutencao.Cancelada;
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
    Programada = 1,
    Pendente = 2,
    EmAndamento = 3,
    Concluida = 5,
    Cancelada = 6
}
#endregion