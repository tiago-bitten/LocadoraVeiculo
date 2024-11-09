using System.Text.RegularExpressions;

namespace Locadora.Veiculo.Helpers;

public static class PlacaHelper
{
    private static readonly Regex RegexPlaca = new Regex(
        @"^[A-Z]{3}-[0-9]{4}$|^[A-Z]{3}[0-9][A-Z][0-9]{2}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static bool ValidarPlaca(string placa)
    {
        return !string.IsNullOrWhiteSpace(placa) && RegexPlaca.IsMatch(placa);
    }
}