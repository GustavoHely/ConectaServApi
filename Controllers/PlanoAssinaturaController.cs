using ConectaServApi.Data;
using ConectaServApi.DTOs;
using ConectaServApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanoAssinaturaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlanoAssinaturaController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cadastra um novo plano de assinatura.
        /// Todos os campos (Nome, Valor e DuracaoEmDias) são obrigatórios.
        /// Este plano será utilizado por prestadores ao realizar pagamentos.
        /// </summary>
        /// <param name="dto">Dados do plano</param>
        /// <returns>Plano criado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Cadastrar(PlanoAssinaturaDTO dto)
        {
            var plano = new PlanoAssinatura
            {
                Nome = dto.Nome,
                Valor = dto.Valor,
                DuracaoEmDias = dto.DuracaoEmDias
            };

            _context.PlanosAssinatura.Add(plano);
            await _context.SaveChangesAsync();

            dto.Id = plano.Id;
            return CreatedAtAction(nameof(ObterPorId), new { id = plano.Id }, dto);
        }

        /// <summary>
        /// Lista todos os planos de assinatura disponíveis.
        /// </summary>
        /// <returns>Lista de planos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PlanoAssinaturaDTO>>> Listar()
        {
            return await _context.PlanosAssinatura
                .Select(p => new PlanoAssinaturaDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Valor = p.Valor,
                    DuracaoEmDias = p.DuracaoEmDias
                }).ToListAsync();
        }

        /// <summary>
        /// Obtém os dados de um plano específico.
        /// </summary>
        /// <param name="id">ID do plano</param>
        /// <returns>Dados do plano</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlanoAssinaturaDTO>> ObterPorId(int id)
        {
            var p = await _context.PlanosAssinatura.FindAsync(id);
            if (p == null) return NotFound();

            return new PlanoAssinaturaDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Valor = p.Valor,
                DuracaoEmDias = p.DuracaoEmDias
            };
        }

        /// <summary>
        /// Atualiza os dados de um plano existente.
        /// </summary>
        /// <param name="id">ID do plano</param>
        /// <param name="dto">Novos dados</param>
        /// <returns>NoContent se atualizado</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Atualizar(int id, PlanoAssinaturaDTO dto)
        {
            var plano = await _context.PlanosAssinatura.FindAsync(id);
            if (plano == null) return NotFound();

            plano.Nome = dto.Nome;
            plano.Valor = dto.Valor;
            plano.DuracaoEmDias = dto.DuracaoEmDias;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Exclui um plano de assinatura.
        /// </summary>
        /// <param name="id">ID do plano</param>
        /// <returns>NoContent se excluído</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Excluir(int id)
        {
            var plano = await _context.PlanosAssinatura.FindAsync(id);
            if (plano == null) return NotFound();

            _context.PlanosAssinatura.Remove(plano);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
