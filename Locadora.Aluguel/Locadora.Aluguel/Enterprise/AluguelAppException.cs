namespace Locadora.Aluguel.Enterprise;

public class AluguelAppException : Exception
{
    public ETipoException Tipo { get; }

    public AluguelAppException(ETipoException exceptionType) 
        : base(GerarMensagem(exceptionType))
    {
        Tipo = exceptionType;
    }

    public AluguelAppException(ETipoException exceptionType, string customMessage)
        : base($"{GerarMensagem(exceptionType)}: {customMessage}")
    {
        Tipo = exceptionType;
    }

    public AluguelAppException(ETipoException exceptionType, string customMessage, Exception innerException)
        : base($"{GerarMensagem(exceptionType)}: {customMessage}", innerException)
    {
        Tipo = exceptionType;
    }

    private static string GerarMensagem(ETipoException exceptionType)
    {
        return exceptionType switch
        {
            ETipoException.DataInicialMaiorQueDataFinal => "A data inicial não pode ser maior que a data final.",
            ETipoException.ClienteEstaComAluguelEmAndamento => "O cliente está com um aluguel em andamento.",
            ETipoException.VeiculoEstaComAluguelEmAndamento => "O veículo está com um aluguel em andamento.",
            ETipoException.ValorTotalAluguelInvalido => "O valor minimo do aluguel precisa ser mairo que zero.",
            ETipoException.ErroIntegracaoCliente => "Ocorreu um erro na integração com o sistema de clientes.",
            ETipoException.ErroIntegracaoVeiculo => "Ocorreu um erro na integração com o sistema de veículos.",
            ETipoException.ClienteNaoEncontrado => "Cliente não encontrado.",
            ETipoException.VeiculoNaoEncontrado => "Veículo não encontrado.",
            ETipoException.AluguelNaoEncontrado => "Aluguel não encontrado.",
            ETipoException.ClienteNaoValido => "Cliente não é válido.",
            ETipoException.VeiculoNaoValido => "Veículo não é válido.",
            ETipoException.AluguelNaoPodeSerConcluido => "Apenas alugueis em andamento podem ser concluidos.",
            _ => "Ocorreu um erro na aplicação de veículos."
        };
    }
}

public enum ETipoException
{
    DataInicialMaiorQueDataFinal = 1,
    ClienteEstaComAluguelEmAndamento = 2,
    VeiculoEstaComAluguelEmAndamento = 3,
    ValorTotalAluguelInvalido = 4,
    ErroIntegracaoCliente = 5,
    ClienteNaoEncontrado = 6,
    ErroIntegracaoVeiculo = 7,
    VeiculoNaoEncontrado = 8,
    AluguelNaoEncontrado = 9,
    ClienteNaoValido = 10,
    VeiculoNaoValido = 11,
    AluguelNaoPodeSerConcluido = 12
}