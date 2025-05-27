using ConectaServApi.Data;
using ConectaServApi.DTOs;
using ConectaServApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacaoPrestadorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AvaliacaoPrestadorController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registra uma nova avaliação de um cliente para um prestador.
        /// </summary>
        /// <param name="dto">Dados da avaliação</param>
        /// <returns>DTO com ID preenchido</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Cadastrar(AvaliacaoPrestadorDTO dto)
        {
            var avaliacao = new AvaliacaoPrestador
            {
                ClienteId = dto.ClienteId,
                PrestadorId = dto.PrestadorId,
                Nota = dto.Nota,
                Comentario = dto.Comentario,
                DataAvaliacao = dto.DataAvaliacao
            };

            _context.AvaliacoesPrestador.Add(avaliacao);
            await _context.SaveChangesAsync();

            dto.Id = avaliacao.Id;
            return CreatedAtAction(nameof(ObterPorId), new { id = avaliacao.Id }, dto);
        }

        /// <summary>
        /// Lista todas as avaliações de prestadores.
        /// </summary>
        /// <returns>Lista de avaliações</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AvaliacaoPrestadorDTO>>> Listar()
        {
            return await _context.AvaliacoesPrestador
                .Select(a => new AvaliacaoPrestadorDTO
                {
                    Id = a.Id,
                    ClienteId = a.ClienteId,
                    PrestadorId = a.PrestadorId,
                    Nota = a.Nota,
                    Comentario = a.Comentario,
                    DataAvaliacao = a.DataAvaliacao
                }).ToListAsync();
        }

        /// <summary>
        /// Obtém uma avaliação específica pelo ID.
        /// </summary>
        /// <param name="id">ID da avaliação</param>
        /// <returns>DTO da avaliação</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AvaliacaoPrestadorDTO>> ObterPorId(int id)
        {
            var a = await _context.AvaliacoesPrestador.FindAsync(id);
            if (a == null) return NotFound();

            return new AvaliacaoPrestadorDTO
            {
                Id = a.Id,
                ClienteId = a.ClienteId,
                PrestadorId = a.PrestadorId,
                Nota = a.Nota,
                Comentario = a.Comentario,
                DataAvaliacao = a.DataAvaliacao
            };
        }

        /// <summary>
        /// Exclui uma avaliação de prestador.
        /// </summary>
        /// <param name="id">ID da avaliação</param>
        /// <returns>NoContent se removida</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Excluir(int id)
        {
            var a = await _context.AvaliacoesPrestador.FindAsync(id);
            if (a == null) return NotFound();

            _context.AvaliacoesPrestador.Remove(a);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Lista todas as avaliações feitas por um cliente.
        /// </summary>
        /// <param name="clienteId">ID do cliente</param>
        /// <returns>Lista de avaliações</returns>
        [HttpGet("cliente/{clienteId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AvaliacaoPrestadorDTO>>> ListarPorCliente(int clienteId)
        {
            return await _context.AvaliacoesPrestador
                .Where(a => a.ClienteId == clienteId)
                .Select(a => new AvaliacaoPrestadorDTO
                {
                    Id = a.Id,
                    ClienteId = a.ClienteId,
                    PrestadorId = a.PrestadorId,
                    Nota = a.Nota,
                    Comentario = a.Comentario,
                    DataAvaliacao = a.DataAvaliacao
                }).ToListAsync();
        }

        /// <summary>
        /// Lista todas as avaliações recebidas por um prestador.
        /// </summary>
        /// <param name="prestadorId">ID do prestador</param>
        /// <returns>Lista de avaliações</returns>
        [HttpGet("prestador/{prestadorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AvaliacaoPrestadorDTO>>> ListarPorPrestador(int prestadorId)
        {
            return await _context.AvaliacoesPrestador
                .Where(a => a.PrestadorId == prestadorId)
                .Select(a => new AvaliacaoPrestadorDTO
                {
                    Id = a.Id,
                    ClienteId = a.ClienteId,
                    PrestadorId = a.PrestadorId,
                    Nota = a.Nota,
                    Comentario = a.Comentario,
                    DataAvaliacao = a.DataAvaliacao
                }).ToListAsync();
        }
    }
}
