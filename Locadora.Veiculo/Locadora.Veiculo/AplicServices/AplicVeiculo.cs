using AutoMapper;
using Locadora.Cliente.AplicServices.Infra;
using Locadora.Veiculo.Dtos;
using Locadora.Veiculo.Enterprise;
using Locadora.Veiculo.Models;
using Locadora.Veiculo.Repositories.Infra;
using Locadora.Veiculo.Services;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Veiculo.AplicServices;

#region Interface
public interface IAplicVeiculo
{
    Task<ResultadoVeiculoDto> AdicionarAsync(AdicionarVeiculoDto dto);
    Task<ResultadoVeiculoDto> ObterPorIdAsync(string id);
    Task<(List<ResultadoVeiculoDto> Listagem, int Total)> ObterTodosAsync(QueryFiltro filtro);
    Task<(List<ResultadoVeiculoDto> Listagem, int Total)> ObterParaAlugarAsync(QueryObterParaAlugar query);
    Task<VeiculoValidoDto> ValidarParaAlugarAsync(QueryValidarParaAlugar queryValidarParaAlugar);
    Task<ResultadoVeiculoDto> AtualizarAsync(AtualizarVeiculoDto dto);
    Task DefinirStatusAsync(DefinirStatusDto dto);
}
#endregion

public class AplicVeiculo : AplicBase<Models.Veiculo, IServVeiculo>, IAplicVeiculo
{
    #region Ctor
    public AplicVeiculo(IMapper mapper, 
                        IServVeiculo service, 
                        IUnitOfWork uow)
        : base(mapper, service, uow)
    {
    }
    #endregion

    #region AdicionarAsync
    public async Task<ResultadoVeiculoDto> AdicionarAsync(AdicionarVeiculoDto dto)
    {
        var veiculo = Mapper.Map<Models.Veiculo>(dto);
        
        veiculo.ValidarDataFabricao();
        
        await Uow.IniciarTransacaoAsync();
        await Service.AdicionarAsync(veiculo);
        await Uow.PersistirTransacaoAsync();
        
        var resultado = Mapper.Map<ResultadoVeiculoDto>(veiculo);

        return resultado;
    }
    #endregion

    #region ObterPorId
    public async Task<ResultadoVeiculoDto> ObterPorIdAsync(string id)
    {
        var veiculo = await Service.ObterPorIdAsync(id);
        veiculo.ExcecaoSeNulo(ETipoException.VeiculoNaoEncontrado);
        
        var resultado = Mapper.Map<ResultadoVeiculoDto>(veiculo);
        
        return resultado;
    }
    #endregion

    #region ObterTodosAsync
    public async Task<(List<ResultadoVeiculoDto> Listagem, int Total)> ObterTodosAsync(QueryFiltro filtro)
    {
        var query = Service.ObterTodos();
        var total = await query.CountAsync();

        if (filtro.Skip.HasValue)
            query = query.Skip(filtro.Skip.Value);

        if (filtro.Take.HasValue)
            query = query.Take(filtro.Take.Value);

        var lista = await query.ToListAsync();
        var resultado = Mapper.Map<List<ResultadoVeiculoDto>>(lista);

        return (resultado, total);
    }
    #endregion

    #region ObterParaAlugarAsync
    public async Task<(List<ResultadoVeiculoDto> Listagem, int Total)> ObterParaAlugarAsync(QueryObterParaAlugar queryFiltro)
    {
        var query = Service.ObterParaAlugar(queryFiltro.DataInicial, queryFiltro.DataFinal);
        var total = await query.CountAsync();
        
        if (queryFiltro.Skip.HasValue)
            query = query.Skip(queryFiltro.Skip.Value);

        if (queryFiltro.Take.HasValue)
            query = query.Take(queryFiltro.Take.Value);

        var lista = await query.ToListAsync();
        var resultado = Mapper.Map<List<ResultadoVeiculoDto>>(lista);

        return (resultado, total);
    }
    #endregion
    
    #region ValidarParaAlugarAsync
    public async Task<VeiculoValidoDto> ValidarParaAlugarAsync(QueryValidarParaAlugar queryValidarParaAlugar)
    {
        var veiculo = await Service.ObterPorIdAsync(queryValidarParaAlugar.CodigoVeiculo);
        veiculo.ExcecaoSeNulo(ETipoException.VeiculoNaoEncontrado);

        var (valido, mensagem) = await Service.ValidoParaAlugarAsync(veiculo, queryValidarParaAlugar.DataInicial, queryValidarParaAlugar.DataFinal);

        var resultado = new VeiculoValidoDto(valido, mensagem);
        
        return resultado;
    }
    #endregion
    
    #region AtualizarAsync
    public async Task<ResultadoVeiculoDto> AtualizarAsync(AtualizarVeiculoDto dto)
    {
        var veiculo = await Service.ObterPorIdAsync(dto.CodigoVeiculo);
        veiculo.ExcecaoSeNulo(ETipoException.VeiculoNaoEncontrado);
        
        Mapper.Map(dto, veiculo);
        
        veiculo.ValidarDataFabricao();
        
        await Uow.IniciarTransacaoAsync();
        Service.Atualizar(veiculo);
        await Uow.PersistirTransacaoAsync();
        
        var resultado = Mapper.Map<ResultadoVeiculoDto>(veiculo);

        return resultado;
    }
    #endregion
    
    #region DefinidirStatusAsync
    public async Task DefinirStatusAsync(DefinirStatusDto dto)
    {
        var veiculo = await Service.ObterPorIdAsync(dto.CodigoVeiculo);
        veiculo.ExcecaoSeNulo(ETipoException.VeiculoNaoEncontrado);
        
        switch (dto.Status)
        {
            case "Disponivel":
                veiculo.Disponibilizar();
                break;
            case "Alugado":
                veiculo.Alugar();
                break;
            case "Manutencao":
                veiculo.EmManutencao();
                break;
            case "Reservado":
                veiculo.Reservar();
                break;
            case "Vendido":
                veiculo.Vender();
                break;  
            default:
                throw new VeiculoAppException(ETipoException.StatusVeiculoInvalido, "Status de veículo inválido.");
        }
        
        await Uow.IniciarTransacaoAsync();
        Service.Atualizar(veiculo);
        await Uow.PersistirTransacaoAsync();
    }
    #endregion
}