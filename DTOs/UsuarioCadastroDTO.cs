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
        public string NumTelefone { get; set; } = string.Empty;

        public string FotoPerfil { get; set; } = string.Empty;
    }
}
