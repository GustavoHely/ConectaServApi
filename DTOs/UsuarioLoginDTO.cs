using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    public class UsuarioLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Senha { get; set; } = string.Empty;
    }
}
