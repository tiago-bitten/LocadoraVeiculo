using Locadora.Aluguel.Models;
using Locadora.Aluguel.Repositories.Context;
using Locadora.Aluguel.Repositories.Infra;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Aluguel.Repositories;

#region Interface
public interface IRepAluguel : IRepBase<Models.Aluguel>
{
    Task<bool> ClienteEstaComAluguelEmAndamentoAsync(string idCliente);
    Task<bool> VeiculoEstaComAluguelEmAndamentoAsync(string idVeiculo);
}
#endregion

public class RepAluguel : RepBase<Models.Aluguel>, IRepAluguel
{
    public RepAluguel(AluguelDbContext context) : base(context)
    {
    }

    #region ClienteEstaComAluguelEmAndamento
    public Task<bool> ClienteEstaComAluguelEmAndamentoAsync(string idCliente)
    {
        return (from al in DbSet
            where al.CodigoCliente == idCliente
                  && al.Status == EStatusAluguel.EmAndamento
            select 1).AnyAsync();
    }
    #endregion
    
    #region VeiculoEstaComAluguelEmAndamentoAsync
    public Task<bool> VeiculoEstaComAluguelEmAndamentoAsync(string idVeiculo)
    {
        return (from al in DbSet
            where al.CodigoVeiculo == idVeiculo
                  && al.Status == EStatusAluguel.EmAndamento
            select 1).AnyAsync();
    }
    #endregion
}