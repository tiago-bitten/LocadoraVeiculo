using Locadora.Veiculo.Models;
using Locadora.Veiculo.Repositories;
using Locadora.Veiculo.Services.Infra;

namespace Locadora.Veiculo.Services;

#region Inteface
public interface IServManutencao : IServBase<Manutencao>
{
    
}
#endregion

public class ServManutencao : ServBase<Manutencao, IRepManutencao>, IServManutencao
{
    #region Ctor
    public ServManutencao(IRepManutencao repository) : base(repository)
    {
    }
    #endregion
}