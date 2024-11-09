namespace Locadora.Veiculo.Extensions

public static class IdentificadorExtensions
{
	public static void ValidarClienteId(this Guid id)
	{
		ValidarIdComPalavra(id, "Cliente");
	}

	public static void ValidarAluguelId(this Guid id)
	{
		ValidarIdComPalavra(id, "Aluguel");
	}

	public static void ValidarVeiculoId(this Guid id)
	{
		ValidarIdComPalavra(id, "Veiculo");
	}

	private static void ValidarIdComPalavra(Guid id, string palavraEsperada)
	{
		var idString = id.ToString();
		var palavraNoId = idString.Substring(9, 8);

		if (!palavraNoId.Equals(palavraEsperada.PadRight(8, '0'), StringComparison.OrdinalIgnoreCase))
		{
			throw new ArgumentException($"O ID não é válido para {palavraEsperada}. ID esperado no formato: XXXXXXXX-{palavraEsperada.PadRight(8, '0')}-XXXXXXXXXXXX");
		}
	}
}