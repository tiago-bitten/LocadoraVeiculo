namespace Locadora.Cliente.Enterprise;

public class ClienteAppException : Exception
{
    public ETipoException Tipo { get; }

    public ClienteAppException(ETipoException exceptionType) 
        : base(GerarMensagem(exceptionType))
    {
        Tipo = exceptionType;
    }

    public ClienteAppException(ETipoException exceptionType, string customMessage)
        : base($"{GerarMensagem(exceptionType)}: {customMessage}")
    {
        Tipo = exceptionType;
    }

    public ClienteAppException(ETipoException exceptionType, string customMessage, Exception innerException)
        : base($"{GerarMensagem(exceptionType)}: {customMessage}", innerException)
    {
        Tipo = exceptionType;
    }

    private static string GerarMensagem(ETipoException exceptionType)
    {
        return exceptionType switch
        {
            ETipoException.CpfInvalido => "O CPF informado é inválido.",
            ETipoException.EmailInvalido => "O E-mail informado é inválido.",
            ETipoException.DataNascimentoInvalida => "Ninguém nascido nessa data está vivo ta de sacanagem.",
            ETipoException.TelefoneInvalido => "O telefone informado é inválido.",
            ETipoException.ClienteComEmailJaExiste => "Já existe um cliente com esse e-mail registrado.",
            ETipoException.ClienteComCpfJaExiste => "Já existe um cliente com esse cpf registrado.",
            ETipoException.IdadeMinimaDeCadastro => "O cliente precisa ser maior de idade.",
            ETipoException.ClienteNaoEncontrado => "Cliente não encontrado.",
            _ => "Ocorreu um erro na aplicação de veículos."
        };
    }
}

public enum ETipoException
{
    CpfInvalido = 1,
    EmailInvalido = 2,
    DataNascimentoInvalida = 3,
    TelefoneInvalido = 4,
    ClienteComEmailJaExiste = 5,
    ClienteComCpfJaExiste = 6,
    IdadeMinimaDeCadastro = 7,
    ClienteNaoEncontrado = 8
}