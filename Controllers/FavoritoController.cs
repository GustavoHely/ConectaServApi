using ConectaServApi.Data;
using ConectaServApi.DTOs;
using ConectaServApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FavoritoController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona um prestador à lista de favoritos de um cliente.
        /// Este endpoint exige campos obrigatórios ClienteId e PrestadorId, ambos já cadastrados.
        /// </summary>
        /// <param name="dto">Dados do favorito</param>
        /// <returns>ID do favorito criado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Cadastrar(FavoritoDTO dto)
        {
            var favorito = new Favorito
            {
                ClienteId = dto.ClienteId,
                PrestadorId = dto.PrestadorId
            };

            _context.Favoritos.Add(favorito);
            await _context.SaveChangesAsync();

            dto.Id = favorito.Id;
            return CreatedAtAction(nameof(ObterPorId), new { id = favorito.Id }, dto);
        }

        /// <summary>
        /// Lista todos os favoritos cadastrados no sistema.
        /// </summary>
        /// <returns>Lista de favoritos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<FavoritoDTO>>> Listar()
        {
            return await _context.Favoritos
                .Select(f => new FavoritoDTO
                {
                    Id = f.Id,
                    ClienteId = f.ClienteId,
                    PrestadorId = f.PrestadorId
                }).ToListAsync();
        }

        /// <summary>
        /// Obtém os dados de um favorito pelo ID.
        /// </summary>
        /// <param name="id">ID do favorito</param>
        /// <returns>DTO do favorito</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FavoritoDTO>> ObterPorId(int id)
        {
            var favorito = await _context.Favoritos.FindAsync(id);
            if (favorito == null) return NotFound();

            return new FavoritoDTO
            {
                Id = favorito.Id,
                ClienteId = favorito.ClienteId,
                PrestadorId = favorito.PrestadorId
            };
        }

        /// <summary>
        /// Exclui um favorito do sistema.
        /// </summary>
        /// <param name="id">ID do favorito</param>
        /// <returns>NoContent se removido</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Excluir(int id)
        {
            var favorito = await _context.Favoritos.FindAsync(id);
            if (favorito == null) return NotFound();

            _context.Favoritos.Remove(favorito);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Verifica se um prestador já está favoritado por um cliente.
        /// </summary>
        /// <param name="clienteId">ID do cliente</param>
        /// <param name="prestadorId">ID do prestador</param>
        /// <returns>True se for favorito, false caso contrário</returns>
        [HttpGet("existe")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> Existe([FromQuery] int clienteId, [FromQuery] int prestadorId)
        {
            var existe = await _context.Favoritos
                .AnyAsync(f => f.ClienteId == clienteId && f.PrestadorId == prestadorId);

            return Ok(existe);
        }

        /// <summary>
        /// Lista todos os favoritos de um cliente específico.
        /// </summary>
        /// <param name="clienteId">ID do cliente</param>
        /// <returns>Lista de favoritos</returns>
        [HttpGet("cliente/{clienteId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<FavoritoDTO>>> ListarPorCliente(int clienteId)
        {
            return await _context.Favoritos
                .Where(f => f.ClienteId == clienteId)
                .Select(f => new FavoritoDTO
                {
                    Id = f.Id,
                    ClienteId = f.ClienteId,
                    PrestadorId = f.PrestadorId
                }).ToListAsync();
        }

        /// <summary>
        /// Lista todos os favoritos em que um prestador aparece.
        /// </summary>
        /// <param name="prestadorId">ID do prestador</param>
        /// <returns>Lista de favoritos</returns>
        [HttpGet("prestador/{prestadorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<FavoritoDTO>>> ListarPorPrestador(int prestadorId)
        {
            return await _context.Favoritos
                .Where(f => f.PrestadorId == prestadorId)
                .Select(f => new FavoritoDTO
                {
                    Id = f.Id,
                    ClienteId = f.ClienteId,
                    PrestadorId = f.PrestadorId
                }).ToListAsync();
        }
    }
}
