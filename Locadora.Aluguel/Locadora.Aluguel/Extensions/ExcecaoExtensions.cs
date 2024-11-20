using Locadora.Aluguel.Enterprise;

namespace Locadora.Aluguel.Extensions;

public static class ExcecaoExtensions
{
    public static void ExcecaoSeNulo<T>(this T obj, ETipoException tipo, string? mensagem = null) where T : class
    {
        switch (obj)
        {
            case null when !string.IsNullOrWhiteSpace(mensagem):
                throw new AluguelAppException(tipo, mensagem);
            case null:
                throw new AluguelAppException(tipo);
        }
    }
}