using Microsoft.AspNetCore.Mvc;
using ConectaServApi.Models;
using ConectaServApi.DTOs;
using ConectaServApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestadorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrestadorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] PrestadorCadastroDTO dto)
        {
            var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId);
            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            var prestador = new Prestador
            {
                UsuarioId = dto.UsuarioId
            };

            _context.Prestadores.Add(prestador);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Prestador cadastrado com sucesso.", prestador.Id });
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var prestadores = await _context.Prestadores
                .Include(p => p.Usuario)
                .ToListAsync();

            return Ok(prestadores);
        }
    }
}
