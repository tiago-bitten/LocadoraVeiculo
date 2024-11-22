using System;
using Locadora.Veiculo.Models;

namespace Locadora.Veiculo.Enterprise;

public class VeiculoAppException : Exception
{
    public ETipoException Tipo { get; }

    public VeiculoAppException(ETipoException exceptionType) 
        : base(GerarMensagem(exceptionType))
    {
        Tipo = exceptionType;
    }

    public VeiculoAppException(ETipoException exceptionType, string customMessage)
        : base($"{GerarMensagem(exceptionType)}: {customMessage}")
    {
        Tipo = exceptionType;
    }

    public VeiculoAppException(ETipoException exceptionType, string customMessage, Exception innerException)
        : base($"{GerarMensagem(exceptionType)}: {customMessage}", innerException)
    {
        Tipo = exceptionType;
    }

    private static string GerarMensagem(ETipoException exceptionType)
    {
        return exceptionType switch
        {
            ETipoException.VeiculoNaoDisponivel => "O veículo solicitado não está disponível.",
            ETipoException.ManutencaoPendente => "O veículo possui uma manutenção pendente.",
            ETipoException.DataInicialMaiorQueFinal => "A data inicial não pode ser maior que a data final.",
            ETipoException.ManutencaoConcluidaSemDataFinal => "Para concluir uma manutenção, é necessário informar a data final.",
            ETipoException.DataFabricacaoInvalida => "A data de fabricação informada é inválida.",
            ETipoException.TipoModeloIncompativel => "O tipo de veiculo escolhido não é compativel ao modelo selecionado.",
            ETipoException.VeiculoComPlacaJaExiste => "Já existe um veiculo com a placa informada.",
            ETipoException.PlacaInvalida => "A placa informada é inválida.",
            ETipoException.VeiculoNaoEncontrado => "Veículo não encontrado.",
            ETipoException.ManutencaoNaoEncontrada => "Manutenção não encontrada.",
            ETipoException.VeiculoJaPossuiManutencaoProgramada => "Veículo já possui manutenção programada.",
            ETipoException.ManutencaoNaoPodeSerCancelada => "A manutenção não pode ser cancelada.",
            _ => "Ocorreu um erro na aplicação de veículos."
        };
    }
}

public enum ETipoException
{
    VeiculoNaoDisponivel = 1,
    ManutencaoPendente = 2,
    DataInicialMaiorQueFinal = 3,
    ManutencaoConcluidaSemDataFinal = 4,
    DataFabricacaoInvalida = 5,
    TipoModeloIncompativel = 6,
    VeiculoComPlacaJaExiste = 7,
    PlacaInvalida = 8,
    VeiculoNaoEncontrado = 9,
    ManutencaoNaoEncontrada = 10,
    VeiculoJaPossuiManutencaoProgramada = 11,
    ManutencaoNaoPodeSerCancelada = 12
}