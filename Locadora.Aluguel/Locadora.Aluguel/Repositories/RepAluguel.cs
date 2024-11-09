using Locadora.Aluguel.Repositories.Context;
using Locadora.Aluguel.Repositories.Infra;

namespace Locadora.Aluguel.Repositories;

#region Interface
public interface IRepAluguel : IRepBase<Models.Aluguel>
{
    Task<bool> ClienteEstaSemAluguel(string idCliente);
}
#endregion

public class RepAluguel : RepBase<Models.Aluguel>, IRepAluguel
{
    public RepAluguel(AluguelDbContext context) : base(context)
    {
    }

    public Task<bool> ClienteEstaSemAluguel(string idCliente)
    {
        throw new NotImplementedException();
    }
}