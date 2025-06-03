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

        /// <summary>
        /// Retorna o prestador vinculado a um usuário específico.
        /// </summary>
        /// <param name="usuarioId">ID do usuário</param>
        /// <returns>Prestador associado ou 404</returns>
        [HttpGet("buscar-por-usuario/{usuarioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Prestador>> BuscarPorUsuario(int usuarioId)
        {
            var prestador = await _context.Prestadores
                .FirstOrDefaultAsync(p => p.UsuarioId == usuarioId);

            if (prestador == null)
                return NotFound("Prestador não encontrado.");

            return Ok(prestador);
        }

    }
}
