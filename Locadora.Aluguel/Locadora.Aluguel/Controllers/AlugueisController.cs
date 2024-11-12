using Locadora.Aluguel.AplicServices;
using Locadora.Aluguel.Controllers.Infra;
using Locadora.Aluguel.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.Aluguel.Controllers;

[Route("api/[controller]")]
public class AlugueisController : ControllerAluguelBase
{
    #region Ctor
    private readonly IAplicAluguel _aplicAluguel;

    public AlugueisController(IAplicAluguel aplicAluguel)
    {
        _aplicAluguel = aplicAluguel;
    }
    #endregion
    
    #region AdicionarAsync
    [HttpPost("Adicionar")]
    public async Task<IActionResult> AdicionarAsync([FromBody] AdicionarAluguelDto dto)
    {
        try
        {
            var resposta = await _aplicAluguel.AdicionarAsync(dto);

            return RespostaSucesso(resposta, "Aluguel adicionado com sucesso.");
        }
        catch (Exception e)
        {
            return RespostaErro(e.Message);
        }
    }
    #endregion
}