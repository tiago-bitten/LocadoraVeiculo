using AutoMapper;
using Locadora.Cliente.AplicServices.Infra;
using Locadora.Veiculo.Dtos;
using Locadora.Veiculo.Enterprise;
using Locadora.Veiculo.Repositories.Infra;
using Locadora.Veiculo.Services;
using Microsoft.EntityFrameworkCore;

namespace Locadora.Veiculo.AplicServices;

#region Interface
public interface IAplicVeiculo
{
    Task<ResultadoVeiculoDto> AdicionarAsync(AdicionarVeiculoDto dto);
    Task<ResultadoVeiculoDto?> ObterPorIdAsync(string id);
    Task<(List<ResultadoVeiculoDto> Listagem, int Total)> ObterTodosAsync(QueryFiltro? filtro);
    Task<(List<ResultadoVeiculoDto> Listagem, int Total)> ObterParaAlugarAsync(QueryObterParaAlugar query);
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
    public async Task<ResultadoVeiculoDto?> ObterPorIdAsync(string id)
    {
        var veiculo = await Service.ObterPorIdAsync(id);
        return veiculo is not null ? Mapper.Map<ResultadoVeiculoDto>(veiculo) : null;
    }
    #endregion

    #region ObterTodosAsync
    public async Task<(List<ResultadoVeiculoDto> Listagem, int Total)> ObterTodosAsync(QueryFiltro? filtro)
    {
        var query = Service.ObterTodos();
        var total = await query.CountAsync();

        if (filtro?.Skip.HasValue == true)
            query = query.Skip(filtro.Skip.Value);

        if (filtro?.Take.HasValue == true)
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
}
