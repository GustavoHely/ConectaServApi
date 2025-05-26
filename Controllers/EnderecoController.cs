using ConectaServApi.Data;
using ConectaServApi.DTOs;
using ConectaServApi.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public IActionResult CadastrarEndereco([FromBody] EnderecoCadastroDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterPorId), new { id = endereco.Id }, endereco);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var endereco = _context.Enderecos.Find(id);
            if (endereco == null) return NotFound();
            return Ok(endereco);
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            return Ok(_context.Enderecos.ToList());
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] EnderecoCadastroDTO dto)
        {
            var endereco = _context.Enderecos.Find(id);
            if (endereco == null) return NotFound();

            endereco.Estado = dto.Estado;
            endereco.Cidade = dto.Cidade;
            endereco.Bairro = dto.Bairro;
            endereco.Rua = dto.Rua;
            endereco.Numero = dto.Numero;
            endereco.CEP = dto.CEP;
            endereco.Latitude = dto.Latitude;
            endereco.Longitude = dto.Longitude;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            var endereco = _context.Enderecos.Find(id);
            if (endereco == null) return NotFound();

            _context.Enderecos.Remove(endereco);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
