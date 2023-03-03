using System.ComponentModel.DataAnnotations;
using api_flutter.Data;
using api_flutter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_flutter.Controllers
{
    [ApiController]
    [Route("journal")]
    public class JournalController : ControllerBase
    {
        [HttpPost]
        [Route("criar")]
        public async Task<IActionResult> CriarJournal(
       [FromServices] AppDbContext context,
       [FromBody] Journal journalRequest)
        {
            if (journalRequest is null)
            {
                return BadRequest();
            }

            var journal = new Journal
            {
                Content = journalRequest.Content,
                CreatedAt = DateTime.Now,
                UpdateAt = DateTime.Now
            };

            await context.Journals.AddAsync(journal);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("buscar")]
        public async Task<IActionResult> RetornaJournals(
            [FromServices] AppDbContext context
        )
        {
            List<Journal> journals = await context.Journals.ToListAsync();

            if (journals is null)
                return NotFound("Não achamos nenhum Journal");

            return Ok(journals);
        }

        [HttpPut]
        [Route("atualizar")]
        public async Task<IActionResult> AtualizarJournal
        (
            [FromBody][Required] Journal journalRequest,
            [FromServices] AppDbContext context
        )
        {
            var journalGet = await context.Journals.FirstOrDefaultAsync(j => j.Id == journalRequest.Id);

            if (journalGet is null)
                return NotFound();

            journalGet.Content = journalRequest.Content != null ? journalRequest.Content : journalGet.Content;
            journalGet.UpdateAt = DateTime.Now;

            context.Journals.Update(journalGet);
            await context.SaveChangesAsync();
            return Ok("Sucesso em atualizar");
        }

        [HttpDelete]
        [Route("deletar")]
        public async Task<IActionResult> DeletarJournal
        (
            [FromBody][Required] Guid Id,
            [FromServices] AppDbContext context
        )
        {
            var journal = await context.Journals.FindAsync(Id);
            if (journal is null)
                return NotFound();

            context.Journals.Remove(journal);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("teste")]
        public async Task<IActionResult> Teste()
        {
            return Ok("funcionou");
        }
    }
}