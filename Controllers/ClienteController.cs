using ConectaServApi.DTOs;
using ConectaServApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ConectaServApi.Data;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CadastrarCliente([FromBody] ClienteCadastroDTO dto)
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // Verificar se o usuário já é cliente
            if (await _context.Clientes.AnyAsync(c => c.UsuarioId == usuarioId))
                return BadRequest("Cliente já cadastrado.");

            var cliente = new Cliente
            {
                CPF = dto.CPF,
                Telefone = dto.Telefone,
                Celular = dto.Celular,
                UsuarioId = usuarioId
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Cliente cadastrado com sucesso!" });
        }
    }
}
