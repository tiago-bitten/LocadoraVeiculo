﻿using Locadora.Cliente.Enterprise;
using Locadora.Cliente.Repositories;
using Locadora.Cliente.Services.Infra;

namespace Locadora.Cliente.Services;

#region Interface
public interface IServCliente : IServBase<Models.Cliente>
{
}
#endregion

public class ServCliente : ServBase<Models.Cliente, IRepCliente>, IServCliente
{
    #region Ctor
    public ServCliente(IRepCliente repository) : base(repository)
    {
    }
    #endregion

    #region AdicionarAsync
    public override async Task AdicionarAsync(Models.Cliente cliente)
    {
        var existePorCpf = await Repository.ObterPorCpfAsync(cliente.Cpf);
        if (existePorCpf is not null)
            throw new ClienteAppException(ETipoException.ClienteComCpfJaExiste);

        var existePorEmail = await Repository.ObterPorEmailAsync(cliente.Email);
        if (existePorEmail is not null)
            throw new ClienteAppException(ETipoException.ClienteComEmailJaExiste);
        
        cliente.ValidarIdadeMinima();

        await base.AdicionarAsync(cliente);
    }
    #endregion
} 