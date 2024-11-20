using AutoMapper;
using Locadora.Cliente.AplicServices.Infra;
using Locadora.Veiculo.Repositories.Infra;
using Locadora.Veiculo.Services;

namespace Locadora.Veiculo.AplicServices;

public interface IAplicManutencao
{
    
}

public class AplicManutencao : AplicBase<Models.Manutencao, IServManutencao>, IAplicManutencao
{
    #region Ctor
    public AplicManutencao(IMapper mapper,
                           IServManutencao service,
                           IUnitOfWork uow)
        : base(mapper, service, uow)
    {
    }
    #endregion
}