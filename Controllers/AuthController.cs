using ConectaServApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConectaServApi.Models;
using ConectaServApi.Data;
using ConectaServApi.Services;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsuarioCadastroDTO dto)
        {
            // Verifica se email já está cadastrado
            if (await _context.Usuarios.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("E-mail já cadastrado.");

            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Telefone = "",   // Preencher depois se quiser
                Celular = "",
                FotoPerfilUrl = "",
                EnderecoId = 0   // Relacionar quando o endereço estiver pronto
            };

            usuario.DefinirSenha(dto.Senha);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Usuário registrado com sucesso." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO dto, [FromServices] IConfiguration config)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (usuario == null || !usuario.VerificarSenha(dto.Senha))
            {
                return Unauthorized(new { mensagem = "E-mail ou senha inválidos." });
            }

            var token = TokenService.GenerateToken(usuario, config);

            return Ok(new
            {
                token,
                usuario = new { usuario.Id, usuario.Nome, usuario.Email }
            });
        }

    }
}

