using AutoMapper;
using Locadora.Cliente.AplicServices.Infra;
using Locadora.Cliente.Controllers.Infra;
using Locadora.Cliente.Dtos;
using Locadora.Cliente.Enterprise;
using Locadora.Cliente.Extensions;
using Locadora.Cliente.Repositories.Infra;
using Locadora.Cliente.Services;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Cliente.AplicServices;

#region Interface
public interface IAplicCliente
{
    Task<ResultadoClienteDto> AdicionarAsync(AdicionarClienteDto dto);
    Task<ResultadoClienteDto?> ObterPorIdAsync(string id);
    Task<(List<ResultadoClienteDto> Listagem, int Total)> ObterTodosAsync(QueryFiltro? filtro);
    Task<ClienteValidoDto> ValidarParaAlugarAsync(string id);
}
#endregion

public class AplicCliente : AplicBase<Models.Cliente, IServCliente>, IAplicCliente
{
    #region Ctor
    public AplicCliente(IMapper mapper, 
                        IServCliente service,
                        IUnitOfWork uow) 
        : base(mapper, service, uow)
    {
    }
    #endregion

    #region AdicionarAsync
    public async Task<ResultadoClienteDto> AdicionarAsync(AdicionarClienteDto dto)
    {
        var cliente = Mapper.Map<Models.Cliente>(dto);
        
        cliente.ValidarCpf();
        cliente.ValidarEmail();
        cliente.ValidarTelefone();
        cliente.ValidarDataNascimento();
        
        await Uow.IniciarTransacaoAsync();
        await Service.AdicionarAsync(cliente);
        await Uow.PersistirTransacaoAsync();
        
        var resultado = Mapper.Map<ResultadoClienteDto>(cliente);

        return resultado;
    }
    #endregion

    #region ObterPorId

    public async Task<ResultadoClienteDto?> ObterPorIdAsync(string id)
    {
        var cliente = await Service.ObterPorIdAsync(id);

        var resultado = cliente is not null ? Mapper.Map<ResultadoClienteDto>(cliente) : null;

        return resultado;
    }
    #endregion
    
    #region ObterTodosAsync
    public async Task<(List<ResultadoClienteDto> Listagem, int Total)> ObterTodosAsync(QueryFiltro? filtro)
    {
        var query = Service.ObterTodos();
        var total = await query.CountAsync();
        
        if (filtro?.Skip.HasValue == true)
            query = query.Skip(filtro.Skip.Value);

        if (filtro?.Take.HasValue == true)
            query = query.Take(filtro.Take.Value);

        var lista = await query.ToListAsync();
        var resultado = Mapper.Map<List<ResultadoClienteDto>>(lista);

        return (resultado, total);
    }
    #endregion
    
    #region ValidarParaAlugarAsync
    public async Task<ClienteValidoDto> ValidarParaAlugarAsync(string id)
    {
        var cliente = await Service.ObterPorIdAsync(id);
        cliente.ExcecaoSeNulo(ETipoException.ClienteNaoEncontrado);

        var (valido, mensagem) = Service.ValidoParaAlugar(cliente);

        var resposta = new ClienteValidoDto(valido, mensagem);
        
        return resposta;
    }
    #endregion
}