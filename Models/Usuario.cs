using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string FotoPerfilUrl { get; set; }
        public int EnderecoId { get; set; }

        // Métodos de segurança
        public void DefinirSenha(string senha)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(senha);
                var hash = sha256.ComputeHash(bytes);
                SenhaHash = Convert.ToBase64String(hash);
            }
        }

        public bool VerificarSenha(string senha)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(senha);
                var hash = sha256.ComputeHash(bytes);
                var senhaHash = Convert.ToBase64String(hash);
                return SenhaHash == senhaHash;
            }
        }
    }
}
