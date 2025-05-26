using ConectaServApi.Data;
using ConectaServApi.DTOs;
using ConectaServApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatoEmpresaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContatoEmpresaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] ContatoEmpresaCadastroDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contato = new ContatoEmpresa
            {
                EmpresaId = dto.EmpresaId,
                TipoContato = dto.TipoContato,
                Valor = dto.Valor,
                Descricao = dto.Descricao
            };

            _context.ContatosEmpresa.Add(contato);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterPorId), new { id = contato.Id }, contato);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var contato = _context.ContatosEmpresa.Find(id);
            if (contato == null) return NotFound();
            return Ok(contato);
        }

        [HttpGet("empresa/{empresaId}")]
        public IActionResult ListarPorEmpresa(int empresaId)
        {
            var contatos = _context.ContatosEmpresa
                .Where(c => c.EmpresaId == empresaId)
                .ToList();
            return Ok(contatos);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] ContatoEmpresaCadastroDTO dto)
        {
            var contato = _context.ContatosEmpresa.Find(id);
            if (contato == null) return NotFound();

            contato.TipoContato = dto.TipoContato;
            contato.Valor = dto.Valor;
            contato.Descricao = dto.Descricao;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var contato = _context.ContatosEmpresa.Find(id);
            if (contato == null) return NotFound();

            _context.ContatosEmpresa.Remove(contato);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
