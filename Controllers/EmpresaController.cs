using ConectaServApi.Data;
using ConectaServApi.DTOs;
using ConectaServApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmpresaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] EmpresaCadastroDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var empresa = new Empresa
            {
                PrestadorId = dto.PrestadorId,
                Nome = dto.Nome,
                RazaoSocial = dto.RazaoSocial,
                Cnpj = dto.Cnpj
            };

            _context.Empresas.Add(empresa);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterPorId), new { id = empresa.Id }, empresa);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var empresa = _context.Empresas
                .Include(e => e.Contatos)
                .FirstOrDefault(e => e.Id == id);

            if (empresa == null) return NotFound();
            return Ok(empresa);
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var empresas = _context.Empresas.Include(e => e.Contatos).ToList();
            return Ok(empresas);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] EmpresaCadastroDTO dto)
        {
            var empresa = _context.Empresas.Find(id);
            if (empresa == null) return NotFound();

            empresa.Nome = dto.Nome;
            empresa.RazaoSocial = dto.RazaoSocial;
            empresa.Cnpj = dto.Cnpj;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var empresa = _context.Empresas.Find(id);
            if (empresa == null) return NotFound();

            _context.Empresas.Remove(empresa);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
