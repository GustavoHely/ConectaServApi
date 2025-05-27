using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    public class EstatisticasPrestadorDTO
    {
        public int? Id { get; set; }

        [Required]
        public int PrestadorId { get; set; }

        [Required]
        public int TotalFavoritos { get; set; }

        [Required]
        public int TotalVisualizacoes { get; set; }
    }
}
