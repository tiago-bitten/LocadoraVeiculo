using Locadora.Cliente.AplicServices;
using Locadora.Cliente.Controllers.Infra;
using Locadora.Cliente.Dtos;
using Locadora.Cliente.Enterprise;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.Cliente.Controllers;

[Route("api/[controller]")]
public class ClientesController : ControllerClienteBase
{
    #region Ctor
    private readonly IAplicCliente _aplicCliente;

    public ClientesController(IAplicCliente aplicCliente)
    {
        _aplicCliente = aplicCliente;
    }
    #endregion
    
    #region Adicionar
    [HttpPost("[action]")]
    public async Task<IActionResult> Adicionar([FromBody] AdicionarClienteDto dto)
    {
        try
        {
            var resultado = await _aplicCliente.AdicionarAsync(dto);
            return RespostaSucesso(resultado, "Cliente adicionado com sucesso.");
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
            var resultado = await _aplicCliente.ObterPorIdAsync(id);

            return resultado is null ?
                RespostaSemConteudo($"Não foi encontrado o cliente com id {id}.") :
                RespostaSucesso(resultado, "Cliente encontrado com sucesso.");
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
            var (listagem, total) = await _aplicCliente.ObterTodosAsync(filtro);
            return RespostaListagem(listagem, total);
        }
        catch (Exception e)
        {
            return RespostaErro(e.Message);
        }
    }
    #endregion

    #region ValidarClienteParaAlugar
    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> ValidarParaAlugar(string id)
    {
        try
        {
            var resultado = await _aplicCliente.ValidarParaAlugarAsync(id);
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
    public async Task<IActionResult> Atualizar([FromBody] AtualizarClienteDto dto)
    {
        try
        {
            var resposta = await _aplicCliente.AtualizarAsync(dto);
            return RespostaSucesso(resposta, "Cliente atualizado com sucesso.");
        }
        catch (Exception e)
        {
            return RespostaErro(e.Message);
        }
    }
    #endregion
}