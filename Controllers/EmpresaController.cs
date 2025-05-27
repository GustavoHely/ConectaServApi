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

        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] EmpresaCadastroDTO dto)
        {
            var prestador = await _context.Prestadores.FindAsync(dto.PrestadorId);
            if (prestador == null)
                return NotFound("Prestador não encontrado.");

            var empresa = new Empresa
            {
                PrestadorId = dto.PrestadorId,
                Nome = dto.Nome,
                RazaoSocial = dto.RazaoSocial,
                Cnpj = dto.Cnpj,
                FotoEstabelecimentoUrl = dto.FotoEstabelecimentoUrl,
                EnderecoId = dto.EnderecoId
            };

            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Empresa cadastrada com sucesso.", empresa.Id });
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var empresas = await _context.Empresas
                .Include(e => e.Prestador)
                    .ThenInclude(p => p.Usuario)
                .Include(e => e.Endereco)
                .Include(e => e.Contatos)
                .ToListAsync();

            return Ok(empresas);
        }
    }
}
