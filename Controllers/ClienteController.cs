using Microsoft.AspNetCore.Mvc;
using ConectaServApi.Models;
using ConectaServApi.DTOs;
using ConectaServApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cadastra um novo cliente vinculado a um usuário existente.
        /// O campo UsuarioId é obrigatório e deve referenciar um usuário previamente cadastrado.
        /// </summary>
        /// <param name="dto">DTO com os dados do cliente e ID do usuário</param>
        /// <returns>ID do cliente criado e mensagem de sucesso</returns>
        [HttpPost("cadastrar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Cadastrar([FromBody] ClienteCadastroDTO dto)
        {
            var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId);
            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            var cliente = new Cliente
            {
                UsuarioId = dto.UsuarioId
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Cliente cadastrado com sucesso.", cliente.Id });
        }

        /// <summary>
        /// Retorna a lista de todos os clientes cadastrados no sistema.
        /// Cada cliente é retornado com os dados do usuário associado.
        /// </summary>
        /// <returns>Lista de clientes com informações de usuário</returns>
        [HttpGet("listar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Listar()
        {
            var clientes = await _context.Clientes
                .Include(c => c.Usuario)
                .ToListAsync();

            return Ok(clientes);
        }

    }
}
