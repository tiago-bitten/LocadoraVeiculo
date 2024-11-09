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
            _ => "Ocorreu um erro na aplicação de veículos."
        };
    }
}

public enum ETipoException
{
    DataInicialMaiorQueDataFinal = 1
}