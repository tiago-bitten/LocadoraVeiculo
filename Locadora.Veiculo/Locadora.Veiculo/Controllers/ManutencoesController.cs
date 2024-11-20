using Locadora.Veiculo.AplicServices;
using Locadora.Veiculo.Controllers.Infra;
using Microsoft.AspNetCore.Components;

namespace Locadora.Veiculo.Controllers;

[Route("api/[controller]")]
public class ManutencoesController : ControllerVeiculoBase
{
    #region Ctor
    private readonly IAplicManutencao _aplicManutencao;

    public ManutencoesController(IAplicManutencao aplicManutencao)
    {
        _aplicManutencao = aplicManutencao;
    }
    #endregion
}