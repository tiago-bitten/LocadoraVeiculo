using Locadora.Aluguel.Repositories;
using Locadora.Aluguel.Services.Infra;

namespace Locadora.Aluguel.Services;

#region Interface
public interface IServAluguel : IServBase<Models.Aluguel>
{
}
#endregion

public class ServAluguel : ServBase<Models.Aluguel, IRepAluguel>, IServAluguel
{
    #region Ctor
    public ServAluguel(IRepAluguel repository) : base(repository)
    {
    }
    #endregion
}