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

        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] ClienteCadastroDTO dto)
        {
            var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId);
            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            var cliente = new Cliente
            {
                UsuarioId = dto.UsuarioId,
                EnderecoId = dto.EnderecoId
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Cliente cadastrado com sucesso.", cliente.Id });
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var clientes = await _context.Clientes
                .Include(c => c.Usuario)
                .Include(c => c.Endereco)
                .ToListAsync();

            return Ok(clientes);
        }
    }
}
