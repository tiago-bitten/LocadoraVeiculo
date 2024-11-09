using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Locadora.Cliente.Repositories.Infra;

public interface IUnitOfWork : IDisposable
{
	Task IniciarTransacaoAsync();
	Task PersistirTransacaoAsync();
	Task ReverterTransacaoAsync();
}

public class UnitOfWork : IUnitOfWork
{
	private readonly DbContext _context;
	private IDbContextTransaction? _transacao;

	public UnitOfWork(DbContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}

	public async Task IniciarTransacaoAsync()
	{
		if (_transacao != null)
		{
			throw new InvalidOperationException("Uma transação já está em andamento.");
		}

		_transacao = await _context.Database.BeginTransactionAsync();
	}

	public async Task PersistirTransacaoAsync()
	{
		if (_transacao == null)
		{
			throw new InvalidOperationException("Nenhuma transação foi iniciada.");
		}

		try
		{
			await _context.SaveChangesAsync();
			await _transacao.CommitAsync();
		}
		catch
		{
			await ReverterTransacaoAsync();
			throw;
		}
		finally
		{
			_transacao.Dispose();
			_transacao = null;
		}
	}

	public async Task ReverterTransacaoAsync()
	{
		if (_transacao != null)
		{
			await _transacao.RollbackAsync();
			_transacao.Dispose();
			_transacao = null;
		}
	}

	public void Dispose()
	{
		_transacao?.Dispose();
		_context.Dispose();
	}
}