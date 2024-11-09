using System.Linq.Expressions;
using Locadora.Aluguel.Models;
using Locadora.Aluguel.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Aluguel.Repositories.Infra;

#region Interface
public interface IRepBase<T> where T : EntidadeBase
{
    Task AdicionarAsync(T entidade);
    Task<T?> ObterPorIdAsync(string id, params string[]? includes);
    IQueryable<T> ObterTodos(params string[]? includes);
    void Atualizar(T entidade);
    void Remover(T entidade);
    Task<T?> BuscarAsync(Expression<Func<T, bool>> filtro, params string[]? includes);
    Task<bool> ExisteAsync(Expression<Func<T, bool>> filtro);
    Task<int> ContarAsync(Expression<Func<T, bool>> filtro);
}
#endregion

public class RepBase<T> : IRepBase<T> where T : EntidadeBase
{
    #region Ctor
    protected readonly AluguelDbContext Context;
    protected readonly DbSet<T> DbSet;

    public RepBase(AluguelDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }
    #endregion

    #region AdicionarAsync
    public async Task AdicionarAsync(T entidade)
    {
        await DbSet.AddAsync(entidade);
    }
    #endregion

    #region ObterPorIdAsync
    public async Task<T?> ObterPorIdAsync(string id, params string[]? includes)
    {
        return await DbSet.FindAsync(id);
    }
    #endregion

    #region ObterTodos
    public IQueryable<T> ObterTodos(params string[]? includes)
    {
        return DbSet.AsQueryable();
    }
    #endregion

    #region Atualizar
    public void Atualizar(T entidade)
    {
        entidade.Atualizar();
        DbSet.Update(entidade);
    }
    #endregion

    #region Remover
    public void Remover(T entidade)
    {
        DbSet.Remove(entidade);
    }
    #endregion

    #region BuscarAsync
    public async Task<T?> BuscarAsync(Expression<Func<T, bool>> filtro, params string[]? includes)
    {
        return await DbSet.FirstOrDefaultAsync(filtro);
    }
    #endregion

    #region ExisteAsync
    public Task<bool> ExisteAsync(Expression<Func<T, bool>> filtro)
    {
        return DbSet.AnyAsync(filtro);
    }
    #endregion

    #region ContarAsync
    public Task<int> ContarAsync(Expression<Func<T, bool>> filtro)
    {
        return DbSet.CountAsync(filtro);
    }
    #endregion
}
