using Locadora.Cliente.Repositories.Context;
using Locadora.Cliente.Repositories.Infra;

namespace Locadora.Cliente.Repositories;

#region Interface
public interface IRepCliente : IRepBase<Models.Cliente>
{
    Task<Models.Cliente?> ObterPorCpfAsync(string cpf);
    Task<Models.Cliente?> ObterPorEmailAsync(string email);
}
#endregion

public class RepCliente : RepBase<Models.Cliente>, IRepCliente
{
    #region Ctor
    public RepCliente(ClienteDbContext context) : base(context)
    {
    }
    #endregion

    #region RecuperarPorCpfAsync
    public Task<Models.Cliente?> ObterPorCpfAsync(string cpf)
    {
        return BuscarAsync(x => x.Cpf == cpf);
    }
    #endregion

    #region RecuperarPorEmailAsync
    public Task<Models.Cliente?> ObterPorEmailAsync(string email)
    {
        return BuscarAsync(x => x.Email == email);
    }
    #endregion
} 