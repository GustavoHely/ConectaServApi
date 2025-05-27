using ConectaServApi.Data;
using ConectaServApi.DTOs;
using ConectaServApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnderecoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] EnderecoCadastroDTO dto)
        {
            var endereco = new Endereco
            {
                Estado = dto.Estado,
                Cidade = dto.Cidade,
                Bairro = dto.Bairro,
                Rua = dto.Rua,
                Numero = dto.Numero,
                CEP = dto.CEP,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude
            };

            _context.Enderecos.Add(endereco);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Endereço cadastrado com sucesso.", endereco.Id });
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var enderecos = await _context.Enderecos.ToListAsync();
            return Ok(enderecos);
        }
    }
}
