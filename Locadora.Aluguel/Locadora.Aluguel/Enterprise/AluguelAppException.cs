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
            _ => "Ocorreu um erro na aplicação de veículos."
        };
    }
}

public enum ETipoException
{
    DataInicialMaiorQueDataFinal = 1,
    ClienteEstaComAluguelEmAndamento = 2,
    VeiculoEstaComAluguelEmAndamento = 3,
    ValorTotalAluguelInvalido = 4
}