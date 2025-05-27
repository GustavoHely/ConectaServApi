using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConectaServApi.Models
{
    public class Denuncia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DenuncianteId { get; set; }

        [Required]
        public string TipoDenunciante { get; set; } = "Cliente"; // "Cliente" ou "Prestador"

        [Required]
        public int DenunciadoId { get; set; }

        [Required]
        public string TipoDenunciado { get; set; } = "Prestador"; // "Prestador" ou "Cliente"

        [Required]
        public string Motivo { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        [Required]
        public DateTime Data { get; set; }
    }
}
