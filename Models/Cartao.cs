using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConectaServApi.Models
{
    public class Cartao
    {
        [Key]
        public int Id { get; set; }

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

        [ForeignKey("PrestadorId")]
        public Prestador Prestador { get; set; } = null!;
    }
}
