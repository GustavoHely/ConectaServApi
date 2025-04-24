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
    [Route("prestador")]
    public class PrestadorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrestadorController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CadastrarPrestador([FromBody] PrestadorCadastroDTO dto)
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // Verifica se esse usuário já é um prestador
            if (await _context.Prestadores.AnyAsync(p => p.UsuarioId == usuarioId))
                return BadRequest("Usuário já está cadastrado como prestador.");

            var prestador = new Prestador
            {
                Cnpj = dto.Cnpj,
                NomeFantasia = dto.NomeFantasia,
                RazaoSocial = dto.RazaoSocial,
                Telefone = dto.Telefone,
                Celular = dto.Celular,
                Site = dto.Site,
                UsuarioId = usuarioId
            };

            _context.Prestadores.Add(prestador);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Prestador cadastrado com sucesso!" });
        }
    }
}
