using ConectaServApi.Data;
using ConectaServApi.DTOs;
using ConectaServApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DenunciaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DenunciaController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registra uma nova denúncia no sistema.
        /// </summary>
        /// <param name="dto">Dados da denúncia</param>
        /// <returns>Denúncia criada</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Cadastrar(DenunciaDTO dto)
        {
            var denuncia = new Denuncia
            {
                DenuncianteId = dto.DenuncianteId,
                TipoDenunciante = dto.TipoDenunciante,
                DenunciadoId = dto.DenunciadoId,
                TipoDenunciado = dto.TipoDenunciado,
                Motivo = dto.Motivo,
                Descricao = dto.Descricao,
                Data = dto.Data
            };

            _context.Denuncias.Add(denuncia);
            await _context.SaveChangesAsync();

            dto.Id = denuncia.Id;
            return CreatedAtAction(nameof(ObterPorId), new { id = denuncia.Id }, dto);
        }

        /// <summary>
        /// Lista todas as denúncias registradas.
        /// </summary>
        /// <returns>Lista de denúncias</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DenunciaDTO>>> Listar()
        {
            return await _context.Denuncias
                .Select(d => new DenunciaDTO
                {
                    Id = d.Id,
                    DenuncianteId = d.DenuncianteId,
                    TipoDenunciante = d.TipoDenunciante,
                    DenunciadoId = d.DenunciadoId,
                    TipoDenunciado = d.TipoDenunciado,
                    Motivo = d.Motivo,
                    Descricao = d.Descricao,
                    Data = d.Data
                }).ToListAsync();
        }

        /// <summary>
        /// Obtém os dados de uma denúncia específica.
        /// </summary>
        /// <param name="id">ID da denúncia</param>
        /// <returns>Denúncia correspondente</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DenunciaDTO>> ObterPorId(int id)
        {
            var d = await _context.Denuncias.FindAsync(id);
            if (d == null) return NotFound();

            return new DenunciaDTO
            {
                Id = d.Id,
                DenuncianteId = d.DenuncianteId,
                TipoDenunciante = d.TipoDenunciante,
                DenunciadoId = d.DenunciadoId,
                TipoDenunciado = d.TipoDenunciado,
                Motivo = d.Motivo,
                Descricao = d.Descricao,
                Data = d.Data
            };
        }

        /// <summary>
        /// Exclui uma denúncia do sistema.
        /// </summary>
        /// <param name="id">ID da denúncia</param>
        /// <returns>NoContent se excluída</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Excluir(int id)
        {
            var d = await _context.Denuncias.FindAsync(id);
            if (d == null) return NotFound();

            _context.Denuncias.Remove(d);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Lista todas as denúncias feitas por um determinado usuário.
        /// </summary>
        /// <param name="id">ID do denunciante</param>
        /// <returns>Lista de denúncias feitas</returns>
        [HttpGet("denunciante/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DenunciaDTO>>> ListarPorDenunciante(int id)
        {
            return await _context.Denuncias
                .Where(d => d.DenuncianteId == id)
                .Select(d => new DenunciaDTO
                {
                    Id = d.Id,
                    DenuncianteId = d.DenuncianteId,
                    TipoDenunciante = d.TipoDenunciante,
                    DenunciadoId = d.DenunciadoId,
                    TipoDenunciado = d.TipoDenunciado,
                    Motivo = d.Motivo,
                    Descricao = d.Descricao,
                    Data = d.Data
                }).ToListAsync();
        }

        /// <summary>
        /// Lista todas as denúncias recebidas por um determinado usuário.
        /// </summary>
        /// <param name="id">ID do denunciado</param>
        /// <returns>Lista de denúncias recebidas</returns>
        [HttpGet("denunciado/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DenunciaDTO>>> ListarPorDenunciado(int id)
        {
            return await _context.Denuncias
                .Where(d => d.DenunciadoId == id)
                .Select(d => new DenunciaDTO
                {
                    Id = d.Id,
                    DenuncianteId = d.DenuncianteId,
                    TipoDenunciante = d.TipoDenunciante,
                    DenunciadoId = d.DenunciadoId,
                    TipoDenunciado = d.TipoDenunciado,
                    Motivo = d.Motivo,
                    Descricao = d.Descricao,
                    Data = d.Data
                }).ToListAsync();
        }
    }
}
