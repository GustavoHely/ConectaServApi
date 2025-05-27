using ConectaServApi.Data;
using ConectaServApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServicoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] Servico servico)
        {
            var empresa = await _context.Empresas.FindAsync(servico.EmpresaId);
            if (empresa == null)
                return NotFound("Empresa não encontrada.");

            _context.Servicos.Add(servico);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Serviço cadastrado com sucesso.", servico.Id });
        }

        [HttpGet("listar/{empresaId}")]
        public async Task<IActionResult> ListarPorEmpresa(int empresaId)
        {
            var servicos = await _context.Servicos
                .Where(s => s.EmpresaId == empresaId && s.Ativo)
                .ToListAsync();

            return Ok(servicos);
        }
    }
}