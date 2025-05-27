using ConectaServApi.Data;
using ConectaServApi.DTOs;
using ConectaServApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AgendamentoController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cadastra um novo agendamento entre cliente e prestador.
        /// </summary>
        /// <param name="dto">Objeto contendo ClienteId, ServicoId, DataHora e Status</param>
        /// <returns>Retorna o ID do agendamento criado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Cadastrar(AgendamentoDTO dto)
        {
            var agendamento = new Agendamento
            {
                ClienteId = dto.ClienteId,
                ServicoId = dto.ServicoId,
                DataHora = dto.DataHora,
                Status = dto.Status
            };

            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();

            dto.Id = agendamento.Id;
            return CreatedAtAction(nameof(ObterPorId), new { id = agendamento.Id }, dto);
        }

        /// <summary>
        /// Lista todos os agendamentos existentes.
        /// </summary>
        /// <returns>Lista de agendamentos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AgendamentoDTO>>> Listar()
        {
            return await _context.Agendamentos
                .Select(a => new AgendamentoDTO
                {
                    Id = a.Id,
                    ClienteId = a.ClienteId,
                    ServicoId = a.ServicoId,
                    DataHora = a.DataHora,
                    Status = a.Status
                }).ToListAsync();
        }

        /// <summary>
        /// Obtém os dados de um agendamento específico.
        /// </summary>
        /// <param name="id">ID do agendamento</param>
        /// <returns>Dados do agendamento</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AgendamentoDTO>> ObterPorId(int id)
        {
            var a = await _context.Agendamentos.FindAsync(id);
            if (a == null) return NotFound();

            return new AgendamentoDTO
            {
                Id = a.Id,
                ClienteId = a.ClienteId,
                ServicoId = a.ServicoId,
                DataHora = a.DataHora,
                Status = a.Status
            };
        }

        /// <summary>
        /// Atualiza o status de um agendamento.
        /// </summary>
        /// <param name="id">ID do agendamento</param>
        /// <param name="dto">DTO contendo o novo status</param>
        /// <returns>NoContent se atualizado com sucesso</returns>
        [HttpPut("{id}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AtualizarStatus(int id, [FromBody] AgendamentoStatusDTO dto)
        {
            var a = await _context.Agendamentos.FindAsync(id);
            if (a == null) return NotFound();

            a.Status = dto.Status;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Cancela um agendamento (muda status para 'Cancelado').
        /// </summary>
        /// <param name="id">ID do agendamento</param>
        /// <returns>NoContent se cancelado</returns>
        [HttpPut("{id}/cancelar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CancelarAgendamento(int id)
        {
            var a = await _context.Agendamentos.FindAsync(id);
            if (a == null) return NotFound();

            a.Status = "Cancelado";
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Exclui um agendamento do sistema.
        /// </summary>
        /// <param name="id">ID do agendamento</param>
        /// <returns>NoContent se removido</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Excluir(int id)
        {
            var a = await _context.Agendamentos.FindAsync(id);
            if (a == null) return NotFound();

            _context.Agendamentos.Remove(a);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Lista todos os agendamentos de um cliente específico.
        /// </summary>
        /// <param name="clienteId">ID do cliente</param>
        /// <returns>Lista de agendamentos</returns>
        [HttpGet("cliente/{clienteId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AgendamentoDTO>>> ListarPorCliente(int clienteId)
        {
            return await _context.Agendamentos
                .Where(a => a.ClienteId == clienteId)
                .Select(a => new AgendamentoDTO
                {
                    Id = a.Id,
                    ClienteId = a.ClienteId,
                    ServicoId = a.ServicoId,
                    DataHora = a.DataHora,
                    Status = a.Status
                }).ToListAsync();
        }

        /// <summary>
        /// Lista todos os agendamentos de um serviço específico.
        /// </summary>
        /// <param name="servicoId">ID do serviço</param>
        /// <returns>Lista de agendamentos</returns>
        [HttpGet("servico/{servicoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AgendamentoDTO>>> ListarPorServico(int servicoId)
        {
            return await _context.Agendamentos
                .Where(a => a.ServicoId == servicoId)
                .Select(a => new AgendamentoDTO
                {
                    Id = a.Id,
                    ClienteId = a.ClienteId,
                    ServicoId = a.ServicoId,
                    DataHora = a.DataHora,
                    Status = a.Status
                }).ToListAsync();
        }
    }
}
