using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ConectaServApi.Data;
using ConectaServApi.Models;
using ConectaServApi.DTOs;

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
        [HttpPost("cadastrar")]
        public IActionResult CadastrarCliente([FromBody] ClienteCadastroDTO dto)
        {
            var usuarioId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value ?? "0");

            var cliente = new Cliente
            {
                UsuarioId = usuarioId,
                Telefone = dto.Telefone,
                Celular = dto.Celular,
                EnderecoId = dto.EnderecoId,
                FotoEstabelecimentoUrl = dto.FotoEstabelecimentoUrl
            };

            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return Ok(new { mensagem = "Cliente cadastrado com sucesso.", cliente.Id });
        }
    }
}
