using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BCrypt.Net;

namespace ConectaServApi.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string SenhaHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(11)]
        public string CPF { get; set; } = string.Empty;


        [Required]
        public string Telefone { get; set; } = string.Empty;

        [Required]
        public string Celular { get; set; } = string.Empty;

        public string FotoPerfilUrl { get; set; } = string.Empty;

        public int EnderecoId { get; set; }

        public void DefinirSenha(string senha)
        {
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public bool VerificarSenha(string senha)
        {
            return BCrypt.Net.BCrypt.Verify(senha, SenhaHash);
        }
    }
}
