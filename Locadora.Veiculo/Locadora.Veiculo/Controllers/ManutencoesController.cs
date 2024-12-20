﻿using Locadora.Veiculo.AplicServices;
using Locadora.Veiculo.Controllers.Infra;
using Locadora.Veiculo.Dtos;
using Locadora.Veiculo.Enterprise;
using Microsoft.AspNetCore.Mvc;

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
    
    #region Adicionar
    [HttpPost("[action]")]
    public async Task<IActionResult> Adicionar([FromBody] CriarManutencaoDto dto)
    {
        try
        {
            var resultado = await _aplicManutencao.AdicionarAsync(dto);
            return RespostaSucesso(resultado, "Manutenção adicionada com sucesso.");
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
            var resultado = await _aplicManutencao.ObterPorIdAsync(id);

            return RespostaSucesso(resultado, "Manutenção encontrada com sucesso.");
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
            var (listagem, total) = await _aplicManutencao.ObterTodosAsync(filtro);

            return RespostaListagem(listagem, total);
        }
        catch (Exception e)
        {
            return RespostaErro(e.Message);
        }
    }
    #endregion
    
    #region Concluir
    [HttpPut("[action]")]
    public async Task<IActionResult> Concluir([FromBody] ConcluirManutencaoDto dto)
    {
        try
        {
            await _aplicManutencao.ConcluirAsync(dto);
            return RespostaSemConteudo("Veículo concluído com sucesso.");
        }
        catch (Exception e)
        {
            return RespostaErro(e.Message);
        }
    }
    #endregion
    
    #region Cancelar
    [HttpPut("[action]")]
    public async Task<IActionResult> Cancelar([FromBody] CancelarManutencaoDto dto)
    {
        try
        {
            await _aplicManutencao.CancelarAsync(dto);
            return RespostaSemConteudo("Manutenção cancelada com sucesso.");
        }
        catch (Exception e)
        {
            return RespostaErro(e.Message);
        }
    }
    #endregion
}