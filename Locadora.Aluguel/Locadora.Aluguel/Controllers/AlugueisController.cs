﻿using Locadora.Aluguel.AplicServices;
using Locadora.Aluguel.Controllers.Infra;
using Locadora.Aluguel.Dtos;
using Locadora.Aluguel.Enterprise;
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
    [HttpPost("[action]")]
    public async Task<IActionResult> Adicionar([FromBody] AdicionarAluguelDto dto)
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
    
    #region ObterPorId
    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(string id)
    {
        try
        {
            var resposta = await _aplicAluguel.ObterPorIdAsync(id);

            return RespostaSucesso(resposta, "Aluguel obtido com sucesso.");
        }
        catch (Exception e)
        {
            return RespostaErro(e.Message);
        }
    }
    #endregion
    
    #region ObterTodos
    [HttpGet]
    public async Task<IActionResult> ObterTodos([FromQuery] QueryFiltro queryFiltro)
    {
        try
        {
            var resposta = await _aplicAluguel.ObterTodosAsync(queryFiltro);

            return RespostaListagem(resposta.Listagem, resposta.Total, "Alugueis obtidos com sucesso.");
        }
        catch (Exception e)
        {
            return RespostaErro(e.Message);
        }
    }
    #endregion
}