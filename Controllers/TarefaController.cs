using System.ComponentModel.DataAnnotations;
using api_flutter.Data;
using api_flutter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_flutter.Controllers;

[ApiController]
[Route("tarefa")]
public class TarefaController : ControllerBase
{

    private readonly AppDbContext _context;

    public TarefaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("criar")]
    public async Task<IActionResult> CriarTarefa(
        [FromBody] Tarefa tarefa)
    {
        if (tarefa == null)
        {
            return BadRequest();
        }

        await _context.Tarefas.AddAsync(tarefa);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    [Route("buscar")]
    public async Task<IActionResult> RetornaTarefas()
    {
        List<Tarefa> tarefas = await _context.Tarefas.ToListAsync();

        if (tarefas is null)
            return NotFound("Não achamos nenhuma Tarefa");

        return Ok(tarefas);
    }

    [HttpPut]
    [Route("atualizar")]
    public async Task<IActionResult> AtualizarTarefa
    (
        [FromBody][Required] Tarefa tarefaRequest
    )
    {
        var tarefaGet = await _context.Tarefas.FirstOrDefaultAsync(tarefa => tarefa.Titulo == tarefaRequest.Titulo);

        if (tarefaGet == null)
            return NotFound("Tarefa não existe ou não foi encontrada!!!");

        tarefaGet.Titulo = tarefaRequest.Titulo != null ? tarefaRequest.Titulo : tarefaGet.Titulo;
        tarefaGet.Dificuldade = tarefaRequest.Dificuldade != null ? tarefaRequest.Dificuldade : tarefaGet.Dificuldade;
        tarefaGet.UrlFoto = tarefaRequest.UrlFoto != null ? tarefaRequest.UrlFoto : tarefaGet.UrlFoto;

        _context.Tarefas.Update(tarefaGet);
        await _context.SaveChangesAsync();
        return Ok("Sucesso em atualizar");
    }

    [HttpDelete]
    [Route("deletar")]
    public async Task<IActionResult> DeletarTarefa
    (
        [FromBody][Required] string titulo    
    )
    {
        var tarefa = await _context.Tarefas.FindAsync(titulo);
        if (tarefa is null)
            return NotFound();

        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();
        return Ok();
    }
}