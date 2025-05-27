using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConectaServApi.Models
{
    public class AvaliacaoPrestador
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; } = null!;

        [Required]
        public int PrestadorId { get; set; }

        [ForeignKey("PrestadorId")]
        public Prestador Prestador { get; set; } = null!;

        [Required]
        [Range(1, 5)]
        public int Nota { get; set; }

        public string Comentario { get; set; } = string.Empty;

        [Required]
        public DateTime DataAvaliacao { get; set; }
    }
}
