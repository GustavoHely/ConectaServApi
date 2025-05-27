using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    public class CartaoCadastroDTO
    {
        [Required]
        public string NomeTitular { get; set; } = string.Empty;

        [Required]
        [StringLength(16)]
        public string Numero { get; set; } = string.Empty;

        [Required]
        [StringLength(3)]
        public string CVC { get; set; } = string.Empty;

        [Required]
        public DateTime Validade { get; set; }

        [Required]
        public int PrestadorId { get; set; }
    }
}
