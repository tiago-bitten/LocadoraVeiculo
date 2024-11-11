using AutoMapper;
using Locadora.Aluguel.Models;
using Locadora.Aluguel.Repositories.Infra;
using Locadora.Aluguel.Services.Infra;

namespace Locadora.Aluguel.AplicServices.Infra
{
    public abstract class AplicBase<TEntidade, TServ>
        where TEntidade : EntidadeBase
        where TServ : IServBase<TEntidade>
    {
        protected readonly IMapper Mapper;
        protected readonly TServ Service;
        protected readonly IUnitOfWork Uow;

        protected AplicBase(IMapper mapper, TServ service, IUnitOfWork uow)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            Service = service ?? throw new ArgumentNullException(nameof(service));
            Uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }
    }
}