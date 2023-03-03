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
    [HttpPost]
    [Route("criar")]
    public async Task<IActionResult> CriarTarefa(
        [FromServices] AppDbContext context,
        [FromBody] Tarefa tarefa)
    {
        if (tarefa == null)
        {
            return BadRequest();
        }

        await context.Tarefas.AddAsync(tarefa);
        await context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    [Route("buscar")]
    public async Task<IActionResult> RetornaTarefas(
        [FromServices] AppDbContext context
    )
    {
        List<Tarefa> tarefas = await context.Tarefas.ToListAsync();

        if (tarefas is null)
            return NotFound("Não achamos nenhuma Tarefa");

        return Ok(tarefas);
    }

    [HttpPut]
    [Route("atualizar")]
    public async Task<IActionResult> AtualizarTarefa
    (
        [FromBody][Required] Tarefa tarefaRequest,
        [FromServices] AppDbContext context
    )
    {
        var tarefaGet = await context.Tarefas.FirstOrDefaultAsync(tarefa => tarefa.Titulo == tarefaRequest.Titulo);

        if (tarefaGet == null)
            return NotFound("Tarefa não existe ou não foi encontrada!!!");

        tarefaGet.Titulo = tarefaRequest.Titulo != null ? tarefaRequest.Titulo : tarefaGet.Titulo;
        tarefaGet.Dificuldade = tarefaRequest.Dificuldade != null ? tarefaRequest.Dificuldade : tarefaGet.Dificuldade;
        tarefaGet.UrlFoto = tarefaRequest.UrlFoto != null ? tarefaRequest.UrlFoto : tarefaGet.UrlFoto;

        context.Tarefas.Update(tarefaGet);
        await context.SaveChangesAsync();
        return Ok("Sucesso em atualizar");
    }

    [HttpDelete]
    [Route("deletar")]
    public async Task<IActionResult> DeletarTarefa
    (
        [FromBody][Required] string titulo,
        [FromServices] AppDbContext context
    )
    {
        var tarefa = await context.Tarefas.FindAsync(titulo);
        if (tarefa is null)
            return NotFound();

        context.Tarefas.Remove(tarefa);
        await context.SaveChangesAsync();
        return Ok();
    }
}