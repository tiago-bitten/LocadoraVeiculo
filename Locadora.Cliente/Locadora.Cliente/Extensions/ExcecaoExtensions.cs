using Locadora.Cliente.Enterprise;

namespace Locadora.Cliente.Extensions;

public static class ExcecaoExtensions
{
    public static void ExcecaoSeNulo<T>(this T? obj, ETipoException tipo, string? mensagem = null) where T : class?
    {
        switch (obj)
        {
            case null when !string.IsNullOrWhiteSpace(mensagem):
                throw new ClienteAppException(tipo, mensagem);
            case null:
                throw new ClienteAppException(tipo);
        }
    }
}