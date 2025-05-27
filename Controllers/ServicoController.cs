using ConectaServApi.Data;
using ConectaServApi.DTOs;
using ConectaServApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServicoController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cadastra um novo serviço para uma empresa.
        /// Este endpoint exige um campo obrigatório EmpresaId, que deve estar previamente cadastrado.
        /// </summary>
        /// <param name="dto">DTO contendo os dados do serviço</param>
        /// <returns>Retorna o ID do novo serviço criado</returns>
        [HttpPost("cadastrar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CadastrarServico(ServicoCadastroDTO dto)
        {
            var servico = new Servico
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Preco = dto.Preco,
                PrecoSobConsulta = dto.PrecoSobConsulta,
                Ativo = dto.Ativo,
                EmpresaId = dto.EmpresaId
            };

            _context.Servicos.Add(servico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterDetalhes), new { id = servico.Id }, new { servico.Id });
        }

        /// <summary>
        /// Lista todos os serviços vinculados a uma empresa.
        /// </summary>
        /// <param name="empresaId">ID da empresa</param>
        /// <returns>Lista de serviços</returns>
        [HttpGet("listar/{empresaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ServicoDetalhadoDTO>>> ListarPorEmpresa(int empresaId)
        {
            return await _context.Servicos
                .Where(s => s.EmpresaId == empresaId)
                .Include(s => s.Empresa)
                .Select(servico => new ServicoDetalhadoDTO
                {
                    Id = servico.Id,
                    Nome = servico.Nome,
                    Descricao = servico.Descricao,
                    Preco = servico.Preco,
                    PrecoSobConsulta = servico.PrecoSobConsulta,
                    Ativo = servico.Ativo,
                    EmpresaId = servico.EmpresaId,
                    EmpresaNome = servico.Empresa.Nome
                }).ToListAsync();
        }

        /// <summary>
        /// Obtém os detalhes completos de um serviço.
        /// </summary>
        /// <param name="id">ID do serviço</param>
        /// <returns>DTO detalhado do serviço</returns>
        [HttpGet("detalhes/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterDetalhes(int id)
        {
            var servico = await _context.Servicos
                .Include(s => s.Empresa)
                    .ThenInclude(e => e.Endereco)
                .Include(s => s.Empresa)
                    .ThenInclude(e => e.Contatos)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (servico == null)
                return NotFound("Serviço não encontrado.");

            var dto = new ServicoDetalhadoDTO
            {
                Id = servico.Id,
                Nome = servico.Nome,
                Descricao = servico.Descricao,
                Preco = servico.Preco,
                PrecoSobConsulta = servico.PrecoSobConsulta,
                Ativo = servico.Ativo,
                EmpresaId = servico.Empresa?.Id ?? 0,
                EmpresaNome = servico.Empresa?.Nome ?? "Empresa não informada",
                RazaoSocial = servico.Empresa?.RazaoSocial ?? "Não informada",
                Cnpj = servico.Empresa?.Cnpj ?? "Não informado",
                FotoEstabelecimentoUrl = servico.Empresa?.FotoEstabelecimentoUrl ?? string.Empty,
                EnderecoCompleto = servico.Empresa?.Endereco != null
                    ? $"{servico.Empresa.Endereco.Rua}, {servico.Empresa.Endereco.Numero} - {servico.Empresa.Endereco.Bairro}, {servico.Empresa.Endereco.Cidade} - {servico.Empresa.Endereco.Estado}, CEP: {servico.Empresa.Endereco.CEP}"
                    : "Endereço não cadastrado",
                Contatos = servico.Empresa?.Contatos != null
                    ? servico.Empresa.Contatos.Select(c => $"{c.TipoContato}: {c.Valor}").ToList()
                    : new List<string>()
            };

            return Ok(dto);
        }

        /// <summary>
        /// Atualiza um serviço existente.
        /// </summary>
        /// <param name="id">ID do serviço</param>
        /// <param name="dto">Novos dados</param>
        /// <returns>NoContent se atualizado</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AtualizarServico(int id, ServicoCadastroDTO dto)
        {
            var servico = await _context.Servicos.FindAsync(id);
            if (servico == null)
                return NotFound("Serviço não encontrado.");

            servico.Nome = dto.Nome;
            servico.Descricao = dto.Descricao;
            servico.Preco = dto.Preco;
            servico.PrecoSobConsulta = dto.PrecoSobConsulta;
            servico.Ativo = dto.Ativo;
            servico.EmpresaId = dto.EmpresaId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Exclui um serviço do sistema.
        /// </summary>
        /// <param name="id">ID do serviço</param>
        /// <returns>NoContent se excluído</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ExcluirServico(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            if (servico == null)
                return NotFound("Serviço não encontrado.");

            _context.Servicos.Remove(servico);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
