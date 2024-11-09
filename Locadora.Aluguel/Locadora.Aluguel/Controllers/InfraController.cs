using Microsoft.AspNetCore.Mvc;

namespace Locadora.Aluguel.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InfraController : ControllerBase
{
    [HttpGet("HealthCheck")]
    public IActionResult Check()
    {
        return Ok("Healthy");
    }
}