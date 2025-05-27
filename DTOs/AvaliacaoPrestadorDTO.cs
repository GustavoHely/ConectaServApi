using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    public class AvaliacaoPrestadorDTO
    {
        public int? Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int PrestadorId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Nota { get; set; }

        public string Comentario { get; set; } = string.Empty;

        [Required]
        public DateTime DataAvaliacao { get; set; }
    }
}
