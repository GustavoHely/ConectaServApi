using ConectaServApi.Data;
using ConectaServApi.DTOs;
using ConectaServApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartaoController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cadastra um novo cartão para um prestador.
        /// </summary>
        /// <param name="dto">Dados do cartão</param>
        /// <returns>ID do cartão criado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Cadastrar(CartaoCadastroDTO dto)
        {
            var cartao = new Cartao
            {
                NomeTitular = dto.NomeTitular,
                Numero = dto.Numero,
                CVC = dto.CVC,
                Validade = dto.Validade,
                PrestadorId = dto.PrestadorId
            };

            _context.Cartoes.Add(cartao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterPorId), new { id = cartao.Id }, new { cartao.Id });
        }

        /// <summary>
        /// Lista todos os cartões cadastrados no sistema.
        /// </summary>
        /// <returns>Lista de cartões (com número mascarado)</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CartaoDetalhadoDTO>>> Listar()
        {
            return await _context.Cartoes
                .Select(c => new CartaoDetalhadoDTO
                {
                    Id = c.Id,
                    NomeTitular = c.NomeTitular,
                    FinalCartao = $"**** **** **** {c.Numero.Substring(c.Numero.Length - 4)}",
                    Validade = c.Validade,
                    PrestadorId = c.PrestadorId
                }).ToListAsync();
        }

        /// <summary>
        /// Obtém os dados de um cartão específico.
        /// </summary>
        /// <param name="id">ID do cartão</param>
        /// <returns>Cartão com número final mascarado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CartaoDetalhadoDTO>> ObterPorId(int id)
        {
            var c = await _context.Cartoes.FindAsync(id);
            if (c == null) return NotFound();

            return new CartaoDetalhadoDTO
            {
                Id = c.Id,
                NomeTitular = c.NomeTitular,
                FinalCartao = $"**** **** **** {c.Numero.Substring(c.Numero.Length - 4)}",
                Validade = c.Validade,
                PrestadorId = c.PrestadorId
            };
        }

        /// <summary>
        /// Atualiza os dados de um cartão.
        /// </summary>
        /// <param name="id">ID do cartão</param>
        /// <param name="dto">Novos dados</param>
        /// <returns>NoContent se atualizado</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Atualizar(int id, CartaoCadastroDTO dto)
        {
            var cartao = await _context.Cartoes.FindAsync(id);
            if (cartao == null) return NotFound();

            cartao.NomeTitular = dto.NomeTitular;
            cartao.Numero = dto.Numero;
            cartao.CVC = dto.CVC;
            cartao.Validade = dto.Validade;
            cartao.PrestadorId = dto.PrestadorId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Exclui um cartão do sistema.
        /// </summary>
        /// <param name="id">ID do cartão</param>
        /// <returns>NoContent se removido</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Excluir(int id)
        {
            var cartao = await _context.Cartoes.FindAsync(id);
            if (cartao == null) return NotFound();

            _context.Cartoes.Remove(cartao);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Lista cartões vinculados a um prestador específico.
        /// </summary>
        /// <param name="prestadorId">ID do prestador</param>
        /// <returns>Cartões do prestador</returns>
        [HttpGet("prestador/{prestadorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CartaoDetalhadoDTO>>> ListarPorPrestador(int prestadorId)
        {
            return await _context.Cartoes
                .Where(c => c.PrestadorId == prestadorId)
                .Select(c => new CartaoDetalhadoDTO
                {
                    Id = c.Id,
                    NomeTitular = c.NomeTitular,
                    FinalCartao = $"**** **** **** {c.Numero.Substring(c.Numero.Length - 4)}",
                    Validade = c.Validade,
                    PrestadorId = c.PrestadorId
                }).ToListAsync();
        }
    }
}
