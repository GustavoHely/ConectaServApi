using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    public class PlanoAssinaturaDTO
    {
        public int? Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public double Valor { get; set; }

        [Required]
        public int DuracaoEmDias { get; set; }
    }
}
