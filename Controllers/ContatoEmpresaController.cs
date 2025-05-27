using ConectaServApi.Data;
using ConectaServApi.DTOs;
using ConectaServApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] ContatoEmpresaCadastroDTO dto)
        {
            var empresa = await _context.Empresas.FindAsync(dto.EmpresaId);
            if (empresa == null)
                return NotFound("Empresa não encontrada.");

            var contato = new ContatoEmpresa
            {
                EmpresaId = dto.EmpresaId,
                TipoContato = dto.TipoContato,
                Valor = dto.Valor,
                Descricao = dto.Descricao
            };

            _context.ContatosEmpresa.Add(contato);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Contato cadastrado com sucesso.", contato.Id });
        }

        [HttpGet("listar/{empresaId}")]
        public async Task<IActionResult> ListarPorEmpresa(int empresaId)
        {
            var contatos = await _context.ContatosEmpresa
                .Where(c => c.EmpresaId == empresaId)
                .ToListAsync();

            return Ok(contatos);
        }
    }
}
