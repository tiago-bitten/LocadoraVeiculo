using Locadora.Cliente.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Locadora.Cliente.Repositories.Infra;

public interface IRepBase<T> where T : EntidadeBase
{
    Task AdicionarAsync(T entidade);

    Task<T?> ObterPorIdAsync(Guid id);

    IQueryable<T> ObterTodos();

    void Atualizar(T entidade);

    void Remover(T entidade);

    Task<bool> ExisteAsync(Expression<Func<T, bool>> filtro);

    Task<int> ContarAsync(Expression<Func<T, bool>> filtro);
}

public class RepBase<T> : IRepBase<T> where T : EntidadeBase
{
    protected readonly DbContext Context;
    protected readonly DbSet<T> DbSet;

    public RepBase(DbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public async Task AdicionarAsync(T entidade)
    {
        await DbSet.AddAsync(entidade);
    }

    public async Task<T?> ObterPorIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public IQueryable<T> ObterTodos()
    {
        return DbSet.AsQueryable();
    }

    public void Atualizar(T entidade)
    {
        entidade.Atualizar();
        DbSet.Update(entidade);
    }

    public void Remover(T entidade)
    {
        DbSet.Remove(entidade);
    }

    public async Task<bool> ExisteAsync(Expression<Func<T, bool>> filtro)
    {
        return await DbSet.AnyAsync(filtro);
    }

    public async Task<int> ContarAsync(Expression<Func<T, bool>> filtro)
    {
        return await DbSet.CountAsync(filtro);
    }
}