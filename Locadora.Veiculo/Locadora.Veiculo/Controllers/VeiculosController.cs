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
    [HttpPost("Adicionar")]
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

            return resultado is null ?
                RespostaSemConteudo($"Não foi encontrado o veículo com id {id}.") :
                RespostaSucesso(resultado, "Veículo encontrado com sucesso.");
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
            var resultado = await _aplicVeiculo.ObterTodosAsync(filtro);
            return RespostaListagem(resultado.Listagem, resultado.Total, 
                "Listagem de veiculos realizdada com sucesso.");
        }
        catch (Exception e)
        {
            return RespostaErro(e.Message);
        }
    }
    #endregion

    #region RecuperarParaAlugar
    public async Task<IActionResult> ObterParaAlugar([FromQuery] QueryObterParaAlugar query)
    {
        try
        {
            var resultado = await _aplicVeiculo.ObterParaAlugarAsync(query);
            return RespostaListagem(resultado.Listagem, resultado.Total,
                "Listagem de veiculos disponível para aluguel realizada com sucesso.");
        }
        catch (Exception e)
        {
            return RespostaErro(e.Message);
        }
    }
    #endregion
}