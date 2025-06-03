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

        /// <summary>
        /// Cadastra um novo prestador vinculado a um usuário existente.
        /// O campo UsuarioId é obrigatório e deve referenciar um usuário previamente cadastrado.
        /// </summary>
        /// <param name="dto">DTO contendo o ID do usuário</param>
        /// <returns>ID do prestador criado e mensagem de sucesso</returns>
        [HttpPost("cadastrar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Retorna a lista de todos os prestadores cadastrados no sistema.
        /// Cada prestador é retornado com os dados do usuário associado.
        /// </summary>
        /// <returns>Lista de prestadores com informações de usuário</returns>
        [HttpGet("listar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Listar()
        {
            var prestadores = await _context.Prestadores
                .Include(p => p.Usuario)
                .ToListAsync();

            return Ok(prestadores);
        }

        /// <summary>
        /// Retorna o prestador vinculado a um usuário específico.
        /// O campo usuarioId deve ser o ID de um usuário já existente no sistema.
        /// </summary>
        /// <param name="usuarioId">ID do usuário</param>
        /// <returns>Prestador associado ao usuário ou erro 404 se não encontrado</returns>
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
