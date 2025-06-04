using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConectaServApi.Data;
using ConectaServApi.DTOs;
using ConectaServApi.Models;
using ConectaServApi.Services;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly GoogleMapsService _mapsService;

        public EnderecoController(AppDbContext context, GoogleMapsService mapsService)
        {
            _context = context;
            _mapsService = mapsService;
        }

        /// <summary>
        /// Cadastra um novo endereço. Preenche latitude/longitude e dados do local com base no CEP, se necessário.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Endereco), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CadastrarEndereco([FromBody] EnderecoCadastroDTO dto)
        {
            // Validar apenas o CEP inicialmente
            if (string.IsNullOrWhiteSpace(dto.CEP))
                return BadRequest(new { errors = new { CEP = new[] { "O CEP é obrigatório." } } });

            var endereco = new Endereco
            {
                Rua = dto.Rua,
                Estado = dto.Estado,
                Cidade = dto.Cidade,
                Bairro = dto.Bairro,
                Numero = dto.Numero,
                CEP = dto.CEP,
                Complemento = dto.Complemento,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude
            };

            // Buscar informações do Google Maps primeiro
            if (string.IsNullOrWhiteSpace(endereco.Rua) ||
                endereco.Latitude == null || endereco.Longitude == null ||
                string.IsNullOrWhiteSpace(endereco.Estado) ||
                string.IsNullOrWhiteSpace(endereco.Cidade) ||
                string.IsNullOrWhiteSpace(endereco.Bairro))
            {
                var resultado = await _mapsService.ObterCoordenadasPorCepAsync(endereco.CEP);

                if (resultado.Latitude == null || resultado.Longitude == null)
                    return BadRequest("CEP inválido ou não encontrado.");

                endereco.Latitude = resultado.Latitude;
                endereco.Longitude = resultado.Longitude;
                endereco.Estado = string.IsNullOrWhiteSpace(endereco.Estado) ? resultado.Estado ?? string.Empty : endereco.Estado;
                endereco.Cidade = string.IsNullOrWhiteSpace(endereco.Cidade) ? resultado.Cidade ?? string.Empty : endereco.Cidade;
                endereco.Bairro = string.IsNullOrWhiteSpace(endereco.Bairro) ? resultado.Bairro ?? string.Empty : endereco.Bairro;
                endereco.Rua = string.IsNullOrWhiteSpace(endereco.Rua) ? resultado.Rua ?? string.Empty : endereco.Rua;
            }

            // Validar o modelo completo após preenchimento
            if (string.IsNullOrWhiteSpace(endereco.Rua))
                return BadRequest(new { errors = new { Rua = new[] { "A rua é obrigatória e não pôde ser obtida automaticamente." } } });

            _context.Enderecos.Add(endereco);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(BuscarPorId), new { id = endereco.Id }, endereco);
        }

        /// <summary>
        /// Retorna um endereço pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Endereco), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            // Use uma consulta SQL direta para evitar problemas de conversão de tipo
            var endereco = await _context.Enderecos
                .FromSqlRaw("SELECT * FROM Enderecos WHERE Id = {0}", id)
                .FirstOrDefaultAsync();

            if (endereco == null)
                return NotFound();

            return Ok(endereco);
        }

        /// <summary>
        /// Atualiza um endereço existente. Também pode completar os dados com base no CEP se latitude/longitude ou informações regionais estiverem ausentes.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AtualizarEndereco(int id, [FromBody] EnderecoCadastroDTO dto)
        {
            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null)
                return NotFound();

            endereco.Rua = dto.Rua;
            endereco.Estado = dto.Estado;
            endereco.Cidade = dto.Cidade;
            endereco.Bairro = dto.Bairro;
            endereco.Numero = dto.Numero;
            endereco.CEP = dto.CEP;
            endereco.Complemento = dto.Complemento;
            endereco.Latitude = dto.Latitude;
            endereco.Longitude = dto.Longitude;

            // Corrigir coordenadas inválidas (0,0)
            if (endereco.Latitude == 0 && endereco.Longitude == 0)
            {
                endereco.Latitude = null;
                endereco.Longitude = null;
            }

            // Buscar informações complementares se necessário
            if (!endereco.Latitude.HasValue || !endereco.Longitude.HasValue ||
                string.IsNullOrWhiteSpace(endereco.Estado) ||
                string.IsNullOrWhiteSpace(endereco.Cidade) ||
                string.IsNullOrWhiteSpace(endereco.Bairro) ||
                string.IsNullOrWhiteSpace(endereco.Rua))
            {
                var resultado = await _mapsService.ObterCoordenadasPorCepAsync(endereco.CEP);

                if (resultado.Latitude == null || resultado.Longitude == null)
                    return BadRequest("CEP inválido ou não encontrado.");

                endereco.Latitude = resultado.Latitude;
                endereco.Longitude = resultado.Longitude;
                endereco.Estado = string.IsNullOrWhiteSpace(endereco.Estado) ? resultado.Estado ?? string.Empty : endereco.Estado;
                endereco.Cidade = string.IsNullOrWhiteSpace(endereco.Cidade) ? resultado.Cidade ?? string.Empty : endereco.Cidade;
                endereco.Bairro = string.IsNullOrWhiteSpace(endereco.Bairro) ? resultado.Bairro ?? string.Empty : endereco.Bairro;
                endereco.Rua = string.IsNullOrWhiteSpace(endereco.Rua) ? resultado.Rua ?? string.Empty : endereco.Rua;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
