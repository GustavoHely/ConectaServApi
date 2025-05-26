using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ConectaServApi.Data;
using ConectaServApi.Models;
using ConectaServApi.DTOs;

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
        [HttpPost("cadastrar")]
        public IActionResult CadastrarPrestador([FromBody] PrestadorCadastroDTO dto)
        {
            var usuarioId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value ?? "0");

            var prestador = new Prestador
            {
                UsuarioId = usuarioId,
                Cnpj = dto.Cnpj,
                RazaoSocial = dto.RazaoSocial,
                Telefone = dto.Telefone,
                Celular = dto.Celular,
                EnderecoId = dto.EnderecoId,
                FotoEstabelecimentoUrl = dto.FotoEstabelecimentoUrl,
                Destaque = false
            };

            _context.Prestadores.Add(prestador);
            _context.SaveChanges();

            return Ok(new { mensagem = "Prestador cadastrado com sucesso.", prestador.Id });
        }
    }
}
