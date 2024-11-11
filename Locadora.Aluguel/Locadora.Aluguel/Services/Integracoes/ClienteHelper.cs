namespace Locadora.Aluguel.Services.Integracoes;

#region Interface
public interface IClienteHelper
{
    Task<ResultadoClienteDto> ObterPorIdAsync(string codigoCliente);
}
#endregion

public class ClienteHelper : IClienteHelper
{
    #region Constants
    private string 
    #endregion
    
    #region Ctor
    private readonly HttpClient _httpClient;

    public ClienteHelper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    #endregion
    
    #region ObterPorIdAsync
    public async Task<ResultadoClienteDto> ObterPorIdAsync(string codigoCliente)
    {
        throw new NotImplementedException();
    }
    #endregion
}

#region Dtos

#region ResultadoClienteDto
public record ResultadoClienteDto(
    string Id,
    string Nome,
    string Cpf,
    string Email,
    DateTime DataNascimento,
    string Telefone,
    string Endereco);

#endregion
#endregion