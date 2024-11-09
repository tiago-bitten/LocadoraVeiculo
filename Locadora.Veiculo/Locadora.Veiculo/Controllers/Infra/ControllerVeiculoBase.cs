using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.Veiculo.Controllers.Infra;

[ApiController]
public abstract class ControllerVeiculoBase : ControllerBase
{
    #region RespostaSucesso
    protected IActionResult RespostaSucesso<T>(T conteudo, string mensagem = "Operação realizada com sucesso")
        where T : class
    {
        var resposta = new RespostaBase<T>
        {
            Sucesso = true,
            Conteudo = conteudo,
            Mensagem = mensagem
        };
        return Ok(resposta);
    }
    #endregion

    #region RespostaSemConteudo
    protected IActionResult RespostaSemConteudo(string mensagem = "Sem conteúdo")
    {
        var resposta = new RespostaBase<dynamic>
        {
            Sucesso = true,
            Mensagem = mensagem
        };

        return Ok(resposta);
    }
    #endregion

    #region RespostaErro
    protected IActionResult RespostaErro(string mensagem, int statusCode = 400)
    {
        var resposta = new RespostaBase<object>
        {
            Sucesso = false,
            Mensagem = mensagem
        };
        return StatusCode(statusCode, resposta);
    }
    #endregion

    #region RespostaListagem
    protected IActionResult RespostaListagem<T>(List<T> conteudo, int total, string mensagem = "Listagem realizada com sucesso")
        where T : class
    {
        var resposta = new RespostaBase<List<T>>
        {
            Sucesso = true,
            Conteudo = conteudo,
            Mensagem = mensagem,
            Total = total
        };
        return Ok(resposta);
    }
    #endregion
}

public class RespostaBase<T> where T : class
{
    public bool Sucesso { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Conteudo { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Mensagem { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Total { get; set; }
}