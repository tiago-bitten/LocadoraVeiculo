using System.Globalization;
using AutoMapper;
using Locadora.Aluguel.AplicServices.Infra;
using Locadora.Aluguel.Dtos;
using Locadora.Aluguel.Repositories.Infra;
using Locadora.Aluguel.Services;
using Locadora.Aluguel.Services.Integracoes;

namespace Locadora.Aluguel.AplicServices;

public interface IAplicAluguel
{
    Task<RespostaAluguelDto> AdicionarAsync(AdicionarAluguelDto dto);
}

public class AplicAluguel : AplicBase<Models.Aluguel, IServAluguel>, IAplicAluguel
{
    #region Ctor
    private readonly IClienteHelper _clienteHelper;
    
    public AplicAluguel(IMapper mapper,
                        IServAluguel service,
                        IUnitOfWork uow,
                        IClienteHelper clienteHelper) 
        : base(mapper, service, uow)
    {
        _clienteHelper = clienteHelper;
    }
    #endregion

    #region AdicionarAsync
    public async Task<RespostaAluguelDto> AdicionarAsync(AdicionarAluguelDto dto)
    {
        var clienteDto = await _clienteHelper.ObterPorIdAsync(dto.CodigoCliente);
        
        var aluguel = Mapper.Map<Models.Aluguel>(dto);
        
        aluguel.ValidarDatas();

        await Uow.IniciarTransacaoAsync();
        await Service.AdicionarAsync(aluguel);
        await Uow.PersistirTransacaoAsync();
        
        var resposta = Mapper.Map<RespostaAluguelDto>(aluguel);

        return resposta;
    }
    #endregion
}