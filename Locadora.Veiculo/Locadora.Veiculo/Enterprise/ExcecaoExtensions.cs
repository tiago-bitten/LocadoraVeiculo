
namespace Locadora.Veiculo.Enterprise;

public static class ExcecaoExtensions
{
    public static void ExcecaoSeNulo<T>(this T obj, ETipoException tipo, string? mensagem = null) where T : class?
    {
        switch (obj)
        {
            case null when !string.IsNullOrWhiteSpace(mensagem):
                throw new VeiculoAppException(tipo, mensagem);
            case null:
                throw new VeiculoAppException(tipo);
        }
    }
}