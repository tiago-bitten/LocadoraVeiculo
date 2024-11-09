namespace Locadora.Cliente.Models;

public class Identificador
{
    public string Id { get; } = GerarId();

    #region GerarId
    private static string GerarId()
    {
        const string palavra = "cliente";

        var random = new Random();
        var numeroAleatorio = random.Next(10000000, 99999999);

        var idFormatado = $"{palavra}{numeroAleatorio}";
        return idFormatado;
    }
    #endregion
}