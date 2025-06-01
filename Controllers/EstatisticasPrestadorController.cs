using ConectaServApi.Data;
using ConectaServApi.DTOs;
using ConectaServApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstatisticasPrestadorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EstatisticasPrestadorController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registra uma nova estatística para um prestador.
        /// Este endpoint exige um campo obrigatório PrestadorId, que deve estar previamente cadastrado.
        /// </summary>
        /// <param name="dto">Dados iniciais da estatística</param>
        /// <returns>Estatística criada</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Criar(EstatisticasPrestadorDTO dto)
        {
            var estat = new EstatisticasPrestador
            {
                PrestadorId = dto.PrestadorId,
                TotalFavoritos = dto.TotalFavoritos,
                TotalVisualizacoes = dto.TotalVisualizacoes
            };

            _context.EstatisticasPrestador.Add(estat);
            await _context.SaveChangesAsync();

            dto.Id = estat.Id;
            return CreatedAtAction(nameof(ObterPorId), new { id = estat.Id }, dto);
        }

        /// <summary>
        /// Lista todas as estatísticas de prestadores.
        /// </summary>
        /// <returns>Lista de estatísticas</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EstatisticasPrestadorDTO>>> Listar()
        {
            return await _context.EstatisticasPrestador
                .Select(e => new EstatisticasPrestadorDTO
                {
                    Id = e.Id,
                    PrestadorId = e.PrestadorId,
                    TotalFavoritos = e.TotalFavoritos,
                    TotalVisualizacoes = e.TotalVisualizacoes
                }).ToListAsync();
        }

        /// <summary>
        /// Obtém estatística de um prestador por ID da estatística.
        /// </summary>
        /// <param name="id">ID da estatística</param>
        /// <returns>Estatística correspondente</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EstatisticasPrestadorDTO>> ObterPorId(int id)
        {
            var estat = await _context.EstatisticasPrestador.FindAsync(id);
            if (estat == null) return NotFound();

            return new EstatisticasPrestadorDTO
            {
                Id = estat.Id,
                PrestadorId = estat.PrestadorId,
                TotalFavoritos = estat.TotalFavoritos,
                TotalVisualizacoes = estat.TotalVisualizacoes
            };
        }

        /// <summary>
        /// Obtém estatística pelo ID do prestador.
        /// </summary>
        /// <param name="prestadorId">ID do prestador</param>
        /// <returns>Estatística do prestador</returns>
        [HttpGet("prestador/{prestadorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EstatisticasPrestadorDTO>> ObterPorPrestador(int prestadorId)
        {
            var estat = await _context.EstatisticasPrestador
                .FirstOrDefaultAsync(e => e.PrestadorId == prestadorId);
            if (estat == null) return NotFound();

            return new EstatisticasPrestadorDTO
            {
                Id = estat.Id,
                PrestadorId = estat.PrestadorId,
                TotalFavoritos = estat.TotalFavoritos,
                TotalVisualizacoes = estat.TotalVisualizacoes
            };
        }

        /// <summary>
        /// Atualiza os valores de estatísticas para um prestador.
        /// </summary>
        /// <param name="id">ID da estatística</param>
        /// <param name="dto">Novos valores</param>
        /// <returns>NoContent se atualizado</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Atualizar(int id, EstatisticasPrestadorDTO dto)
        {
            var estat = await _context.EstatisticasPrestador.FindAsync(id);
            if (estat == null) return NotFound();

            estat.TotalFavoritos = dto.TotalFavoritos;
            estat.TotalVisualizacoes = dto.TotalVisualizacoes;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
