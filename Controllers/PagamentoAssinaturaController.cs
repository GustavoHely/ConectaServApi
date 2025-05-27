using ConectaServApi.Data;
using ConectaServApi.DTOs;
using ConectaServApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentoAssinaturaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PagamentoAssinaturaController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registra um pagamento de assinatura para um prestador.
        /// Este endpoint exige os campos PrestadorId, CartaoId e PlanoAssinaturaId, todos previamente cadastrados.
        /// </summary>
        /// <param name="dto">Dados do pagamento</param>
        /// <returns>Pagamento registrado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Cadastrar(PagamentoAssinaturaDTO dto)
        {
            var pagamento = new PagamentoAssinatura
            {
                PrestadorId = dto.PrestadorId,
                PlanoAssinaturaId = dto.PlanoAssinaturaId,
                CartaoId = dto.CartaoId,
                DataPagamento = dto.DataPagamento,
                Valor = dto.Valor
            };

            _context.PagamentosAssinatura.Add(pagamento);
            await _context.SaveChangesAsync();

            dto.Id = pagamento.Id;
            return CreatedAtAction(nameof(ObterPorId), new { id = pagamento.Id }, dto);
        }

        /// <summary>
        /// Lista todos os pagamentos de assinatura realizados.
        /// </summary>
        /// <returns>Lista de pagamentos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PagamentoAssinaturaDTO>>> Listar()
        {
            return await _context.PagamentosAssinatura
                .Select(p => new PagamentoAssinaturaDTO
                {
                    Id = p.Id,
                    PrestadorId = p.PrestadorId,
                    PlanoAssinaturaId = p.PlanoAssinaturaId,
                    CartaoId = p.CartaoId,
                    DataPagamento = p.DataPagamento,
                    Valor = p.Valor
                }).ToListAsync();
        }

        /// <summary>
        /// Obtém os dados de um pagamento específico.
        /// </summary>
        /// <param name="id">ID do pagamento</param>
        /// <returns>Dados do pagamento</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagamentoAssinaturaDTO>> ObterPorId(int id)
        {
            var p = await _context.PagamentosAssinatura.FindAsync(id);
            if (p == null) return NotFound();

            return new PagamentoAssinaturaDTO
            {
                Id = p.Id,
                PrestadorId = p.PrestadorId,
                PlanoAssinaturaId = p.PlanoAssinaturaId,
                CartaoId = p.CartaoId,
                DataPagamento = p.DataPagamento,
                Valor = p.Valor
            };
        }

        /// <summary>
        /// Lista os pagamentos realizados por um prestador.
        /// </summary>
        /// <param name="prestadorId">ID do prestador</param>
        /// <returns>Lista de pagamentos</returns>
        [HttpGet("prestador/{prestadorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PagamentoAssinaturaDTO>>> ListarPorPrestador(int prestadorId)
        {
            return await _context.PagamentosAssinatura
                .Where(p => p.PrestadorId == prestadorId)
                .Select(p => new PagamentoAssinaturaDTO
                {
                    Id = p.Id,
                    PrestadorId = p.PrestadorId,
                    PlanoAssinaturaId = p.PlanoAssinaturaId,
                    CartaoId = p.CartaoId,
                    DataPagamento = p.DataPagamento,
                    Valor = p.Valor
                }).ToListAsync();
        }

        /// <summary>
        /// Lista os pagamentos realizados para um plano específico.
        /// </summary>
        /// <param name="planoId">ID do plano</param>
        /// <returns>Lista de pagamentos</returns>
        [HttpGet("plano/{planoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PagamentoAssinaturaDTO>>> ListarPorPlano(int planoId)
        {
            return await _context.PagamentosAssinatura
                .Where(p => p.PlanoAssinaturaId == planoId)
                .Select(p => new PagamentoAssinaturaDTO
                {
                    Id = p.Id,
                    PrestadorId = p.PrestadorId,
                    PlanoAssinaturaId = p.PlanoAssinaturaId,
                    CartaoId = p.CartaoId,
                    DataPagamento = p.DataPagamento,
                    Valor = p.Valor
                }).ToListAsync();
        }
    }
}
