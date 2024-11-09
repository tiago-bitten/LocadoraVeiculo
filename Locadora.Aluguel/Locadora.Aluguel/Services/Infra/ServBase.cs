using Locadora.Aluguel.Models;
using System.Linq.Expressions;
using Locadora.Aluguel.Enterprise;
using Locadora.Aluguel.Repositories.Infra;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Aluguel.Services.Infra
{
    #region Interface IServBase
    public interface IServBase<T> where T : EntidadeBase
    {
        Task AdicionarAsync(T entidade);
        Task<T?> ObterPorIdAsync(Guid id);
        Task<List<T>> ObterTodosAsync(QueryFiltro<T>? filtro);
        void Atualizar(T entidade);
        Task RemoverAsync(Guid id);
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

        #region Métodos CRUD
        public async Task AdicionarAsync(T entidade)
        {
            await Repository.AdicionarAsync(entidade);
        }

        public async Task<T?> ObterPorIdAsync(Guid id)
        {
            return await Repository.ObterPorIdAsync(id);
        }

        public async Task<List<T>> ObterTodosAsync(QueryFiltro<T>? filtro)
        {
            var query = Repository.ObterTodos();

            if (filtro?.OrderBy != null)
                query = filtro.OrderByDescending ? query.OrderByDescending(filtro.OrderBy) : query.OrderBy(filtro.OrderBy);

            if (filtro?.Skip.HasValue == true)
                query = query.Skip(filtro.Skip.Value);

            if (filtro?.Take.HasValue == true)
                query = query.Take(filtro.Take.Value);

            return await query.ToListAsync();
        }

        public void Atualizar(T entidade)
        {
            Repository.Atualizar(entidade);
        }

        public async Task RemoverAsync(Guid id)
        {
            var entidade = await Repository.ObterPorIdAsync(id);
            if (entidade != null)
            {
                entidade.Inativar();
                Atualizar(entidade);
            }
        }
        #endregion

        #region Métodos Utilitários
        public async Task<bool> ExisteAsync(Expression<Func<T, bool>> filtro)
        {
            return await Repository.ExisteAsync(filtro);
        }
        #endregion
    }
}
