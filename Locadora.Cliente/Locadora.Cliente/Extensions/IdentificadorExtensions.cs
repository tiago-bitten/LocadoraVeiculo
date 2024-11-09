namespace Locadora.Cliente.Extensions;


public static class IdentificadorExtensions
{
	#region ValidarClienteId
	public static void ValidarClienteId(this Guid id)
	{
		ValidarIdComPalavra(id, "Cliente");
	}
	#endregion

	#region ValidarAluguelId
	public static void ValidarAluguelId(this Guid id)
	{
		ValidarIdComPalavra(id, "Aluguel");
	}
	#endregion

	#region ValidarVeiculoId
	public static void ValidarVeiculoId(this Guid id)
	{
		ValidarIdComPalavra(id, "Veiculo");
	}
	#endregion

	#region ValidarIdComPalavra
	private static void ValidarIdComPalavra(Guid id, string palavraEsperada)
	{
		var idString = id.ToString();
		var palavraNoId = idString.Substring(9, 8);

		if (!palavraNoId.Equals(palavraEsperada.PadRight(8, '0'), StringComparison.OrdinalIgnoreCase))
		{
			throw new ArgumentException($"O ID n�o � v�lido para {palavraEsperada}. ID esperado no formato: XXXXXXXX-{palavraEsperada.PadRight(8, '0')}-XXXXXXXXXXXX");
		}
	}
	#endregion
}