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
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string SenhaHash { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        public string Celular { get; set; }

        [Required]
        public string FotoPerfilUrl { get; set; }

        public int EnderecoId { get; set; }

        // Relacionamentos (se aplicável)
        public ICollection<Cliente>? Clientes { get; set; }
        public ICollection<Prestador>? Prestadores { get; set; }

        // Método para definir senha (hash)
        public void DefinirSenha(string senha)
        {
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha);
        }

        // Método para verificar senha
        public bool VerificarSenha(string senha)
        {
            return BCrypt.Net.BCrypt.Verify(senha, SenhaHash);
        }
    }
}
