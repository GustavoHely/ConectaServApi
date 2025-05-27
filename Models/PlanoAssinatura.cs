using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.Models
{
    public class PlanoAssinatura
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public double Valor { get; set; }

        [Required]
        public int DuracaoEmDias { get; set; } // Ex: 30 para mensal, 180 para semestral
    }
}
