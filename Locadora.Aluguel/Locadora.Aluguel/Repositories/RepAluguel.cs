﻿using Locadora.Aluguel.Models;
using Locadora.Aluguel.Repositories.Context;
using Locadora.Aluguel.Repositories.Infra;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Aluguel.Repositories;

#region Interface
public interface IRepAluguel : IRepBase<Models.Aluguel>
{
    Task<bool> ClienteEstaComAluguelEmAndamentoAsync(string codigoCliente);
    Task<bool> VeiculoEstaComAluguelEmAndamentoAsync(string codigoVeiculo);
}
#endregion

public class RepAluguel : RepBase<Models.Aluguel>, IRepAluguel
{
    public RepAluguel(AluguelDbContext context) : base(context)
    {
    }

    #region ClienteEstaComAluguelEmAndamentoAsync
    public Task<bool> ClienteEstaComAluguelEmAndamentoAsync(string codigoCliente)
    {
        return (from al in DbSet
            where al.CodigoCliente == codigoCliente
                  && al.Status == EStatusAluguel.EmAndamento
            select 1).AnyAsync();
    }
    #endregion
    
    #region VeiculoEstaComAluguelEmAndamentoAsync
    public Task<bool> VeiculoEstaComAluguelEmAndamentoAsync(string codigoVeiculo)
    {
        return (from al in DbSet
            where al.CodigoVeiculo == codigoVeiculo
                  && al.Status == EStatusAluguel.EmAndamento
            select 1).AnyAsync();
    }
    #endregion
}