using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    public class PrestadorCadastroDTO
    {
        [Required]
        public int UsuarioId { get; set; }
    }
}
