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

        private readonly AppDbContext _context;

        public JournalController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("criar")]
        public async Task<IActionResult> CriarJournal(
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

            await _context.Journals.AddAsync(journal);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("buscar")]
        public async Task<IActionResult> RetornaJournals()
        {
            List<Journal> journals = await _context.Journals.ToListAsync();

            if (journals is null)
                return NotFound("Não achamos nenhum Journal");

            return Ok(journals);
        }

        [HttpPut]
        [Route("atualizar")]
        public async Task<IActionResult> AtualizarJournal
        (
            [FromBody][Required] Journal journalRequest
        )
        {
            var journalGet = await _context.Journals.FirstOrDefaultAsync(j => j.Id == journalRequest.Id);

            if (journalGet is null)
                return NotFound();

            journalGet.Content = journalRequest.Content != null ? journalRequest.Content : journalGet.Content;
            journalGet.UpdateAt = DateTime.Now;

            _context.Journals.Update(journalGet);
            await _context.SaveChangesAsync();
            return Ok("Sucesso em atualizar");
        }

        [HttpDelete]
        [Route("deletar")]
        public async Task<IActionResult> DeletarJournal
        (
            [FromBody][Required] Guid Id
        )
        {
            var journal = await _context.Journals.FindAsync(Id);
            if (journal is null)
                return NotFound();

            _context.Journals.Remove(journal);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("teste")]
        public async Task<IActionResult> Teste(
        )
        {
            return Ok("funcionou");
        }
    }
}