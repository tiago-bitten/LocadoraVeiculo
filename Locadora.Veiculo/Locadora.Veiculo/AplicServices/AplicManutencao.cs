using AutoMapper;
using Locadora.Cliente.AplicServices.Infra;
using Locadora.Veiculo.Dtos;
using Locadora.Veiculo.Enterprise;
using Locadora.Veiculo.Models;
using Locadora.Veiculo.Repositories.Infra;
using Locadora.Veiculo.Services;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Veiculo.AplicServices;

public interface IAplicManutencao
{
    Task<ResultadoManutencaoDto> AdicionarAsync(CriarManutencaoDto dto);
    Task<ResultadoManutencaoDto> ObterPorIdAsync(string id);
    Task<(List<ResultadoManutencaoDto> Listagem, int Total)> ObterTodosAsync(QueryFiltro filtro);
    Task ConcluirAsync(string id);
    Task CancelarAsync(CancelarManutencaoDto dto);
}

public class AplicManutencao : AplicBase<Models.Manutencao, IServManutencao>, IAplicManutencao
{
    #region Ctor
    private readonly IServVeiculo _servVeiculo;
    
    public AplicManutencao(IMapper mapper,
                           IServManutencao service,
                           IUnitOfWork uow, 
                           IServVeiculo servVeiculo)
        : base(mapper, service, uow)
    {
        _servVeiculo = servVeiculo;
    }
    #endregion
    
    #region AdicionarAsync
    public async Task<ResultadoManutencaoDto> AdicionarAsync(CriarManutencaoDto dto)
    {
        var manutencao = Mapper.Map<Manutencao>(dto);
        manutencao.ValidarDatas();
        
        var veiculo = await _servVeiculo.ObterPorIdAsync(dto.CodigoVeiculo);
        veiculo.ExcecaoSeNulo(ETipoException.VeiculoNaoEncontrado);
        
        if (dto.DataInicio.Date > DateTime.Now.Date)
        {
            manutencao.Programar();
            veiculo.Reservar();
        }
        else
        {
            manutencao.EmAndamento();
            veiculo.EmManutencao();
        }
        
        await Uow.IniciarTransacaoAsync();
        await Service.AdicionarAsync(manutencao);
        _servVeiculo.Atualizar(veiculo);
        await Uow.PersistirTransacaoAsync();
        
        var resultado = Mapper.Map<ResultadoManutencaoDto>(manutencao);
        
        return resultado;
    }
    #endregion
    
    #region ObterPorIdAsync
    public async Task<ResultadoManutencaoDto> ObterPorIdAsync(string id)
    {
        var manutencao = await Service.ObterPorIdAsync(id);
        manutencao.ExcecaoSeNulo(ETipoException.ManutencaoNaoEncontrada);
        
        var resultado = Mapper.Map<ResultadoManutencaoDto>(manutencao);
        
        return resultado;
    }
    #endregion
    
    #region ObterTodosAsync
    public async Task<(List<ResultadoManutencaoDto> Listagem, int Total)> ObterTodosAsync(QueryFiltro filtro)
    {
        var query = Service.ObterTodos();
        var total = await query.CountAsync();
        
        if (filtro.Skip.HasValue)
        {
            query = query.Skip(filtro.Skip.Value);
        }
        
        if (filtro.Take.HasValue)
        {
            query = query.Take(filtro.Take.Value);
        }
        
        var listagem = await query.ToListAsync();
        var resultado = Mapper.Map<List<ResultadoManutencaoDto>>(listagem);

        return (resultado, total);
    }
    #endregion
    
    #region ConcluirAsync
    public async Task ConcluirAsync(string id)
    {
        var manutencao = await Service.ObterPorIdAsync(id);
        manutencao.ExcecaoSeNulo(ETipoException.ManutencaoNaoEncontrada);
        
        manutencao.Concluir();
        
        await Uow.IniciarTransacaoAsync();
        Service.Atualizar(manutencao);
        
        var veiculo = await _servVeiculo.ObterPorIdAsync(manutencao.CodigoVeiculo);
        veiculo.ExcecaoSeNulo(ETipoException.VeiculoNaoEncontrado);
        
        veiculo.Disponibilizar();
        _servVeiculo.Atualizar(veiculo);
        
        await Uow.PersistirTransacaoAsync();
    }
    #endregion
    
    #region CancelarAsync
    public async Task CancelarAsync(CancelarManutencaoDto dto)
    {
        var manutencao = await Service.ObterPorIdAsync(dto.Id);
        manutencao.ExcecaoSeNulo(ETipoException.ManutencaoNaoEncontrada);

        var veiculo = await _servVeiculo.ObterPorIdAsync(manutencao.CodigoVeiculo);
        veiculo.ExcecaoSeNulo(ETipoException.VeiculoNaoEncontrado);
        
        await Uow.IniciarTransacaoAsync();
        Service.Cancelar(manutencao);
        
        veiculo.Disponibilizar();
        _servVeiculo.Atualizar(veiculo);
        await Uow.PersistirTransacaoAsync();
    }
    #endregion
}