namespace Locadora.Cliente.Models;

public class Identificador
{
    public Guid Id { get; } = GerarId();
    
    #region GerarId
    private static Guid GerarId()
    {
        var guid = Guid.NewGuid().ToString();
        var palavra = "Cliente";
        
        var textoFormatado = palavra.Length > 8
            ? palavra[..8]
            : palavra.PadRight(8, '0');

        var guidPersonalizado = $"{guid[..8]}-{textoFormatado}-{guid[13..]}";
        
        return Guid.Parse(guidPersonalizado);
    }
    #endregion
}