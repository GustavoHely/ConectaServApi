using ConectaServApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConectaServApi.Models;
using ConectaServApi.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ConectaServApi.Services;

namespace ConectaServApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsuarioCadastroDTO dto, [FromServices] IConfiguration config)
        {
            // Verifica se o e-mail já está cadastrado
            if (await _context.Usuarios.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("E-mail já cadastrado.");

            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Telefone = "",     // Pode ser atualizado depois
                Celular = "",
                FotoPerfilUrl = "", // Pode ser atualizado depois
                EnderecoId = 0      // Pode ser atualizado depois
            };

            usuario.DefinirSenha(dto.Senha);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // Gerar token
            var token = TokenService.GenerateToken(usuario, config);

            return Ok(new
            {
                token,
                usuario = new
                {
                    usuario.Id,
                    usuario.Nome,
                    usuario.Email
                }
            });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (usuario == null || !usuario.VerificarSenha(dto.Senha))
                return Unauthorized(new { mensagem = "E-mail ou senha inválidos." });

            var token = GerarToken(usuario);

            return Ok(new
            {
                token,
                usuario = new { usuario.Id, usuario.Nome, usuario.Email }
            });
        }

        private string GerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
                new Claim("id", usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
