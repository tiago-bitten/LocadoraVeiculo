using Locadora.Veiculo.AplicServices;
using Locadora.Veiculo.Controllers.Infra;
using Locadora.Veiculo.Dtos;
using Locadora.Veiculo.Enterprise;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.Veiculo.Controllers;

[Route("api/[controller]")]
public class VeiculosController : ControllerVeiculoBase
{
    #region Ctor
    private readonly IAplicVeiculo _aplicVeiculo;

    public VeiculosController(IAplicVeiculo aplicVeiculo)
    {
        _aplicVeiculo = aplicVeiculo;
    }
    #endregion

    #region Adicionar
    [HttpPost("[action]")]
    public async Task<IActionResult> Adicionar([FromBody] AdicionarVeiculoDto dto)
    {
        try
        {
            var resultado = await _aplicVeiculo.AdicionarAsync(dto);
            return RespostaSucesso(resultado, "Veículo adicionado com sucesso.");
        }
        catch (Exception e)
        {
            return RespostaErro(e.Message);
        }
    }
    #endregion

    #region ObterPorId
    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(string id)
    {
        try
        {
            var resultado = await _aplicVeiculo.ObterPorIdAsync(id);

            return RespostaSucesso(resultado, "Veículo encontrado com sucesso.");
        }
        catch (Exception e)
        {
            return RespostaErro(e.Message);
        }
    }
    #endregion

    #region ObterTodos
    [HttpGet]
    public async Task<IActionResult> ObterTodos([FromQuery] QueryFiltro filtro)
    {
        try
        {
            var (listagem, total) = await _aplicVeiculo.ObterTodosAsync(filtro);
            return RespostaListagem(listagem, total, 
                "Listagem de veiculos realizdada com sucesso.");
        }
        catch (Exception e)
        {
            return RespostaErro(e.Message);
        }
    }
    #endregion

    #region ObterParaAlugar
    [HttpGet("ObterParaAlugar")]
    public async Task<IActionResult> ObterParaAlugar([FromQuery] QueryObterParaAlugar query)
    {
        try
        {
            var (listagem, total) = await _aplicVeiculo.ObterParaAlugarAsync(query);
            return RespostaListagem(listagem, total,
                "Listagem de veiculos disponível para aluguel realizada com sucesso.");
        }
        catch (Exception e)
        {
            return RespostaErro(e.Message);
        }
    }
    #endregion
    
    #region ValidarParaAlugar
    [HttpGet("[action]")]
    public async Task<IActionResult> ValidarParaAlugar([FromQuery] QueryValidarParaAlugar queryValidarParaAlugar)
    {
        try
        {
            var resultado = await _aplicVeiculo.ValidarParaAlugarAsync(queryValidarParaAlugar);
            return RespostaSucesso(resultado);
        }
        catch (Exception e)
        {
            return RespostaErro(e.Message);
        }
    }
    #endregion
    
    #region Atualizar
    [HttpPut("[action]")]
    public async Task<IActionResult> Atualizar([FromBody] AtualizarVeiculoDto dto)
    {
        try
        {
            var resultado = await _aplicVeiculo.AtualizarAsync(dto);
            return RespostaSucesso(resultado, "Veículo atualizado com sucesso.");
        }
        catch (Exception e)
        {
            return RespostaErro(e.Message);
        }
    }
    #endregion
    
    #region DefinirStatus
    [HttpPut("[action]")]
    public async Task<IActionResult> DefinirStatus([FromBody] DefinirStatusDto dto)
    {
        try
        {
            await _aplicVeiculo.DefinirStatusAsync(dto);
            return RespostaSemConteudo("Status do veículo definido com sucesso.");
        }
        catch (Exception e)
        {
            return RespostaErro(e.Message);
        }
    }
    #endregion
}