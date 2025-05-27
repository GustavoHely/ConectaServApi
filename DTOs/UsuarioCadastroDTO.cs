using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    public class UsuarioCadastroDTO
    {
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Senha { get; set; } = string.Empty;

        [Required]
        public string CPF { get; set; } = string.Empty;

        [Required]
        public string Telefone { get; set; } = string.Empty;

        [Required]
        public string Celular { get; set; } = string.Empty;

        public string FotoPerfilUrl { get; set; } = string.Empty;

        public int? EnderecoId { get; set; }
    }
}
