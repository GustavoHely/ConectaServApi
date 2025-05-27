using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConectaServApi.Models
{
    public class PagamentoAssinatura
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PrestadorId { get; set; }

        [ForeignKey("PrestadorId")]
        public Prestador Prestador { get; set; } = null!;

        [Required]
        public int PlanoAssinaturaId { get; set; }

        [ForeignKey("PlanoAssinaturaId")]
        public PlanoAssinatura PlanoAssinatura { get; set; } = null!;

        [Required]
        public int CartaoId { get; set; }

        [ForeignKey("CartaoId")]
        public Cartao Cartao { get; set; } = null!;

        [Required]
        public DateTime DataPagamento { get; set; }

        [Required]
        public double Valor { get; set; }
    }
}
