using AutoMapper;
using Locadora.Aluguel.AplicServices.Infra;
using Locadora.Aluguel.Dtos;
using Locadora.Aluguel.Enterprise;
using Locadora.Aluguel.Extensions;
using Locadora.Aluguel.Models;
using Locadora.Aluguel.Repositories.Infra;
using Locadora.Aluguel.Services;
using Locadora.Aluguel.Services.Integracoes;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Aluguel.AplicServices;

#region Interface
public interface IAplicAluguel
{
    Task<RespostaAluguelDto> AdicionarAsync(AdicionarAluguelDto dto);
    Task<ResultadoVeiculoDto> ObterPorIdAsync(string id);
    Task<(List<RespostaAluguelDto> Listagem, int Total)> ObterTodosAsync(QueryFiltro filtro);
    Task ConcluirAsync(ConcluirAluguelDto dto);
    Task CancelarAsync(CancelarAluguelDto dto);
    Task IniciarProgramadoAsync(IniciarAluguelProgramadoDto dto);
}
#endregion

public class AplicAluguel : AplicBase<Models.Aluguel, IServAluguel>, IAplicAluguel
{
    #region Ctor
    private readonly IClienteHelper _clienteHelper;
    private readonly IVeiculoHelper _veiculoHelper;
    
    public AplicAluguel(IMapper mapper,
                        IServAluguel service,
                        IUnitOfWork uow,
                        IClienteHelper clienteHelper, 
                        IVeiculoHelper veiculoHelper) 
        : base(mapper, service, uow)
    {
        _clienteHelper = clienteHelper;
        _veiculoHelper = veiculoHelper;
    }
    #endregion

    #region AdicionarAsync
    public async Task<RespostaAluguelDto> AdicionarAsync(AdicionarAluguelDto dto)
    {
        var aluguel = Mapper.Map<Models.Aluguel>(dto);
        aluguel.ValidarDatas();
        
        var clienteDto = await _clienteHelper.ObterPorIdAsync(dto.CodigoCliente);
        var veiculoDto = await _veiculoHelper.ObterPorIdAsync(dto.CodigoVeiculo);
        
        clienteDto.ExcecaoSeNulo(ETipoException.ClienteNaoEncontrado);
        veiculoDto.ExcecaoSeNulo(ETipoException.VeiculoNaoEncontrado);
        
        aluguel.ValorTotal = veiculoDto.ValorDiaria * dto.TotalDias;

        if (dto.DataInicio.Date > DateTime.Now.Date)
        {
            aluguel.Programar();
        }
        else
        {
            aluguel.EmAndamento();
        }
        
        await Uow.IniciarTransacaoAsync();
        await Service.AdicionarAsync(aluguel);
        await Uow.PersistirTransacaoAsync();
        
        await _veiculoHelper.DefinirStatusAsync(veiculoDto.Id, "Alugado");
        
        var resposta = Mapper.Map<RespostaAluguelDto>(aluguel);

        return resposta;
    }
    #endregion

    #region ObterPorIdAsync
    public async Task<ResultadoVeiculoDto> ObterPorIdAsync(string id)
    {
        var veiculo = await _veiculoHelper.ObterPorIdAsync(id);
        veiculo.ExcecaoSeNulo(ETipoException.AluguelNaoEncontrado);

        return veiculo;
    }
    #endregion 

    #region ObterTodosAsync
    public async Task<(List<RespostaAluguelDto> Listagem, int Total)> ObterTodosAsync(QueryFiltro queryFiltro)
    {
        var query = Service.ObterTodos();
        var total = await query.CountAsync();

        if (queryFiltro.Skip.HasValue)
        {
            query = query.Skip(queryFiltro.Skip.Value);
        }
        
        if (queryFiltro.Take.HasValue)
        {
            query = query.Take(queryFiltro.Take.Value);
        }
        
        var listagem = await query.ToListAsync();
        var resposta = Mapper.Map<List<RespostaAluguelDto>>(listagem);
        
        return (resposta, total);
    }
    #endregion
    
    #region ConcluirAsync
    public async Task ConcluirAsync(ConcluirAluguelDto dto)
    {
        var aluguel = await Service.ObterPorIdAsync(dto.Id);
        aluguel.ExcecaoSeNulo(ETipoException.AluguelNaoEncontrado);

        await Uow.IniciarTransacaoAsync();
        Service.Concluir(aluguel);
        await Uow.PersistirTransacaoAsync();
        
        await _veiculoHelper.DefinirStatusAsync(aluguel.CodigoVeiculo, "Disponivel");
    }
    #endregion
    
    #region CancelarAsync
    public async Task CancelarAsync(CancelarAluguelDto dto)
    {
        var aluguel = await Service.ObterPorIdAsync(dto.Id);
        aluguel.ExcecaoSeNulo(ETipoException.AluguelNaoEncontrado);

        await Uow.IniciarTransacaoAsync();
        Service.Cancelar(aluguel);
        await Uow.PersistirTransacaoAsync();
        
        await _veiculoHelper.DefinirStatusAsync(aluguel.CodigoVeiculo, "Disponivel");
    }
    #endregion
    
    #region IniciarAluguelProgramadoAsync
    public async Task IniciarProgramadoAsync(IniciarAluguelProgramadoDto dto)
    {
        var aluguel = await Service.ObterPorIdAsync(dto.CodigoAluguel);
        aluguel.ExcecaoSeNulo(ETipoException.AluguelNaoEncontrado);
        
        await Uow.IniciarTransacaoAsync();
        Service.IniciarProgramado(aluguel);
        await Uow.PersistirTransacaoAsync();
    }
    #endregion
}