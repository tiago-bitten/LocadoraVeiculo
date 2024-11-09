using Locadora.Aluguel.Repositories.Context;
using Locadora.Aluguel.Repositories.Infra;

namespace Locadora.Aluguel.Repositories;

public class RepAluguel : RepBase<Models.Aluguel>, IRepAluguel
{
    public RepAluguel(AluguelDbContext context) : base(context)
    {
    }
}