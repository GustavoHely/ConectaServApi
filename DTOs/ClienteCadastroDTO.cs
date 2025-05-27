using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    public class ClienteCadastroDTO
    {
        [Required]
        public int EnderecoId { get; set; }

        [Required]
        public int UsuarioId { get; set; }
    }
}
