using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    public class DenunciaDTO
    {
        public int? Id { get; set; }

        [Required]
        public int DenuncianteId { get; set; }

        [Required]
        public string TipoDenunciante { get; set; } = "Cliente";

        [Required]
        public int DenunciadoId { get; set; }

        [Required]
        public string TipoDenunciado { get; set; } = "Prestador";

        [Required]
        public string Motivo { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        [Required]
        public DateTime Data { get; set; }
    }
}
