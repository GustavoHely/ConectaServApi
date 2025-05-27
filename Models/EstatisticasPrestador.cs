using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConectaServApi.Models
{
    public class EstatisticasPrestador
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PrestadorId { get; set; }

        [ForeignKey("PrestadorId")]
        public Prestador Prestador { get; set; } = null!;

        [Required]
        public int TotalFavoritos { get; set; } = 0;

        [Required]
        public int TotalVisualizacoes { get; set; } = 0;
    }
}
