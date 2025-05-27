using ConectaServApi.Data;
using ConectaServApi.DTOs;
using ConectaServApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmpresaController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cadastra uma nova empresa vinculada a um prestador.
        /// Este endpoint exige um campo obrigatório PrestadorId, que deve estar previamente cadastrado.
        /// </summary>
        /// <param name="dto">DTO com dados da empresa</param>
        /// <returns>Retorna os dados da empresa criada</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Cadastrar(EmpresaCadastroDTO dto)
        {
            var empresa = new Empresa
            {
                Nome = dto.Nome,
                RazaoSocial = dto.RazaoSocial,
                Cnpj = dto.Cnpj,
                FotoEstabelecimentoUrl = dto.FotoEstabelecimentoUrl,
                PrestadorId = dto.PrestadorId
            };

            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();

            dto.Id = empresa.Id;
            return CreatedAtAction(nameof(ObterPorId), new { id = empresa.Id }, dto);
        }

        /// <summary>
        /// Lista todas as empresas cadastradas.
        /// </summary>
        /// <returns>Lista de empresas</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmpresaCadastroDTO>>> Listar()
        {
            return await _context.Empresas
                .Select(e => new EmpresaCadastroDTO
                {
                    Id = e.Id,
                    Nome = e.Nome,
                    RazaoSocial = e.RazaoSocial,
                    Cnpj = e.Cnpj,
                    FotoEstabelecimentoUrl = e.FotoEstabelecimentoUrl,
                    PrestadorId = e.PrestadorId
                }).ToListAsync();
        }

        /// <summary>
        /// Obtém os dados de uma empresa pelo ID.
        /// </summary>
        /// <param name="id">ID da empresa</param>
        /// <returns>Dados da empresa encontrada</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmpresaCadastroDTO>> ObterPorId(int id)
        {
            var e = await _context.Empresas.FindAsync(id);
            if (e == null) return NotFound();

            return new EmpresaCadastroDTO
            {
                Id = e.Id,
                Nome = e.Nome,
                RazaoSocial = e.RazaoSocial,
                Cnpj = e.Cnpj,
                FotoEstabelecimentoUrl = e.FotoEstabelecimentoUrl,
                PrestadorId = e.PrestadorId
            };
        }

        /// <summary>
        /// Atualiza os dados de uma empresa existente.
        /// </summary>
        /// <param name="id">ID da empresa</param>
        /// <param name="dto">Dados atualizados</param>
        /// <returns>NoContent se sucesso</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Atualizar(int id, EmpresaCadastroDTO dto)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null) return NotFound();

            empresa.Nome = dto.Nome;
            empresa.RazaoSocial = dto.RazaoSocial;
            empresa.Cnpj = dto.Cnpj;
            empresa.FotoEstabelecimentoUrl = dto.FotoEstabelecimentoUrl;
            empresa.PrestadorId = dto.PrestadorId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Exclui uma empresa do sistema.
        /// </summary>
        /// <param name="id">ID da empresa</param>
        /// <returns>NoContent se excluída</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Excluir(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null) return NotFound();

            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Lista todas as empresas de um prestador específico.
        /// </summary>
        /// <param name="prestadorId">ID do prestador</param>
        /// <returns>Lista de empresas</returns>
        [HttpGet("prestador/{prestadorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmpresaCadastroDTO>>> ListarPorPrestador(int prestadorId)
        {
            return await _context.Empresas
                .Where(e => e.PrestadorId == prestadorId)
                .Select(e => new EmpresaCadastroDTO
                {
                    Id = e.Id,
                    Nome = e.Nome,
                    RazaoSocial = e.RazaoSocial,
                    Cnpj = e.Cnpj,
                    FotoEstabelecimentoUrl = e.FotoEstabelecimentoUrl,
                    PrestadorId = e.PrestadorId
                }).ToListAsync();
        }
    }
}
