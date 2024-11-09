﻿using AutoMapper;
using Locadora.Cliente.Models;
using Locadora.Cliente.Repositories.Infra;
using Locadora.Cliente.Services.Infra;

namespace Locadora.Cliente.AplicServices.Infra
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