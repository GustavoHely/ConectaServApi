using ConectaServApi.Data;
using ConectaServApi.DTOs;
using ConectaServApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacaoClienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AvaliacaoClienteController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registra uma nova avaliação de um prestador para um cliente.
        /// Este endpoint exige campos obrigatórios PrestadorId e ClienteId, ambos previamente cadastrados.
        /// </summary>
        /// <param name="dto">Dados da avaliação</param>
        /// <returns>DTO com ID preenchido</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Cadastrar(AvaliacaoClienteDTO dto)
        {
            var avaliacao = new AvaliacaoCliente
            {
                PrestadorId = dto.PrestadorId,
                ClienteId = dto.ClienteId,
                Nota = dto.Nota,
                Comentario = dto.Comentario,
                DataAvaliacao = dto.DataAvaliacao
            };

            _context.AvaliacoesCliente.Add(avaliacao);
            await _context.SaveChangesAsync();

            dto.Id = avaliacao.Id;
            return CreatedAtAction(nameof(ObterPorId), new { id = avaliacao.Id }, dto);
        }

        /// <summary>
        /// Lista todas as avaliações de clientes.
        /// </summary>
        /// <returns>Lista de avaliações</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AvaliacaoClienteDTO>>> Listar()
        {
            return await _context.AvaliacoesCliente
                .Select(a => new AvaliacaoClienteDTO
                {
                    Id = a.Id,
                    PrestadorId = a.PrestadorId,
                    ClienteId = a.ClienteId,
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
        public async Task<ActionResult<AvaliacaoClienteDTO>> ObterPorId(int id)
        {
            var a = await _context.AvaliacoesCliente.FindAsync(id);
            if (a == null) return NotFound();

            return new AvaliacaoClienteDTO
            {
                Id = a.Id,
                PrestadorId = a.PrestadorId,
                ClienteId = a.ClienteId,
                Nota = a.Nota,
                Comentario = a.Comentario,
                DataAvaliacao = a.DataAvaliacao
            };
        }

        /// <summary>
        /// Exclui uma avaliação de cliente.
        /// </summary>
        /// <param name="id">ID da avaliação</param>
        /// <returns>NoContent se removida</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Excluir(int id)
        {
            var a = await _context.AvaliacoesCliente.FindAsync(id);
            if (a == null) return NotFound();

            _context.AvaliacoesCliente.Remove(a);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Lista todas as avaliações feitas por um prestador específico.
        /// </summary>
        /// <param name="prestadorId">ID do prestador</param>
        /// <returns>Lista de avaliações</returns>
        [HttpGet("prestador/{prestadorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AvaliacaoClienteDTO>>> ListarPorPrestador(int prestadorId)
        {
            return await _context.AvaliacoesCliente
                .Where(a => a.PrestadorId == prestadorId)
                .Select(a => new AvaliacaoClienteDTO
                {
                    Id = a.Id,
                    PrestadorId = a.PrestadorId,
                    ClienteId = a.ClienteId,
                    Nota = a.Nota,
                    Comentario = a.Comentario,
                    DataAvaliacao = a.DataAvaliacao
                }).ToListAsync();
        }

        /// <summary>
        /// Lista todas as avaliações recebidas por um cliente específico.
        /// </summary>
        /// <param name="clienteId">ID do cliente</param>
        /// <returns>Lista de avaliações</returns>
        [HttpGet("cliente/{clienteId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AvaliacaoClienteDTO>>> ListarPorCliente(int clienteId)
        {
            return await _context.AvaliacoesCliente
                .Where(a => a.ClienteId == clienteId)
                .Select(a => new AvaliacaoClienteDTO
                {
                    Id = a.Id,
                    PrestadorId = a.PrestadorId,
                    ClienteId = a.ClienteId,
                    Nota = a.Nota,
                    Comentario = a.Comentario,
                    DataAvaliacao = a.DataAvaliacao
                }).ToListAsync();
        }
    }
}
