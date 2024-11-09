using System.Linq.Expressions;
using Locadora.Veiculo.Models;
using Locadora.Veiculo.Repositories.Infra;

namespace Locadora.Veiculo.Services.Infra;

#region Interface
public interface IServBase<T> where T : EntidadeBase
{
    Task AdicionarAsync(T entidade);
    Task<T?> ObterPorIdAsync(string id);
    IQueryable<T> ObterTodos(params string[]? includes);
    void Atualizar(T entidade);
    Task RemoverAsync(string id);
    Task<bool> ExisteAsync(Expression<Func<T, bool>> filtro);
}
#endregion

public class ServBase<T, TRep> : IServBase<T> where T : EntidadeBase where TRep : IRepBase<T>
{
    #region Ctor
    protected readonly TRep Repository;

    public ServBase(TRep repository)
    {
        Repository = repository;
    }
    #endregion

    #region AdicionarAsync
    public virtual async Task AdicionarAsync(T entidade)
    {
        await Repository.AdicionarAsync(entidade);
    }
    #endregion

    #region ObterPorIdAsync
    public virtual async Task<T?> ObterPorIdAsync(string id)
    {
        return await Repository.ObterPorIdAsync(id);
    }
    #endregion

    #region ObterTodos
    public virtual IQueryable<T> ObterTodos(params string[]? includes)
    {
        return Repository.ObterTodos(includes);
    }
    #endregion

    #region Atualizar
    public virtual void Atualizar(T entidade)
    {
        Repository.Atualizar(entidade);
    }
    #endregion

    #region RemoverAsync
    public virtual async Task RemoverAsync(string id)
    {
        var entidade = await Repository.ObterPorIdAsync(id);
        if (entidade is not null)
        {
            entidade.Inativar();
            Atualizar(entidade);
        }
    }
    #endregion

    #region ExisteAsync
    public virtual async Task<bool> ExisteAsync(Expression<Func<T, bool>> filtro)
    {
        return await Repository.ExisteAsync(filtro);
    }
    #endregion
}