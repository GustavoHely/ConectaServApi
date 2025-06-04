using ConectaServApi.Data;
using ConectaServApi.DTOs;
using ConectaServApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatoEmpresaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContatoEmpresaController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cadastra um novo contato para uma empresa.
        /// O campo EmpresaId é obrigatório. Tipo de contato deve ser 'telefone', 'whatsapp' ou 'email'.
        /// Caso não exista contato útil ainda, é obrigatório cadastrar pelo menos um WhatsApp ou Email.
        /// </summary>
        /// <param name="dto">DTO com os dados do contato</param>
        /// <returns>Mensagem de sucesso e ID do novo contato</returns>
        [HttpPost("cadastrar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Cadastrar([FromBody] ContatoEmpresaCadastroDTO dto)
        {
            var empresa = await _context.Empresas.FindAsync(dto.EmpresaId);
            if (empresa == null)
                return NotFound("Empresa não encontrada.");

            var tipo = dto.TipoContato.Trim().ToLower();
            var tiposPermitidos = new[] { "telefone", "whatsapp", "email" };

            if (!tiposPermitidos.Contains(tipo))
                return BadRequest("Tipo de contato inválido. Use: telefone, whatsapp ou email.");

            var contatosExistentes = await _context.ContatosEmpresa
                .Where(c => c.EmpresaId == dto.EmpresaId)
                .ToListAsync();

            var temContatoUtil = contatosExistentes.Any(c =>
                c.TipoContato.ToLower() == "whatsapp" || c.TipoContato.ToLower() == "email");

            if (!temContatoUtil && tipo == "telefone")
                return BadRequest("A empresa deve possuir ao menos um contato de WhatsApp ou Email.");

            var contato = new ContatoEmpresa
            {
                EmpresaId = dto.EmpresaId,
                TipoContato = dto.TipoContato,
                Valor = dto.Valor
            };

            _context.ContatosEmpresa.Add(contato);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Contato cadastrado com sucesso.", contato.Id });
        }

        /// <summary>
        /// Lista todos os contatos cadastrados de uma empresa específica.
        /// O ID deve referenciar uma empresa existente no sistema.
        /// </summary>
        /// <param name="empresaId">ID da empresa</param>
        /// <returns>Lista de contatos vinculados à empresa</returns>
        [HttpGet("listar/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarPorEmpresa(int empresaId)
        {
            var contatos = await _context.ContatosEmpresa
                .Where(c => c.EmpresaId == empresaId)
                .ToListAsync();

            return Ok(contatos);
        }

    }
}
