using System.Globalization;
using AutoMapper;
using Locadora.Aluguel.AplicServices.Infra;
using Locadora.Aluguel.Dtos;
using Locadora.Aluguel.Repositories.Infra;
using Locadora.Aluguel.Services;

namespace Locadora.Aluguel.AplicServices;

public interface IAplicAluguel
{
    Task<RespostaAluguelDto> AdicionarAsync(AdicionarAluguelDto dto);
}

public class AplicAluguel : AplicBase<Models.Aluguel, IServAluguel>, IAplicAluguel
{
    #region Ctor
    public AplicAluguel(IMapper mapper,
                        IServAluguel service,
                        IUnitOfWork uow) 
        : base(mapper, service, uow)
    {
    }
    #endregion


    #region AdicionarAsync
    public async Task<RespostaAluguelDto> AdicionarAsync(AdicionarAluguelDto dto)
    {
        var aluguel = Mapper.Map<Models.Aluguel>(dto);

        var resposta = Mapper.Map<RespostaAluguelDto>(aluguel);

        return resposta;
    }
    #endregion
}