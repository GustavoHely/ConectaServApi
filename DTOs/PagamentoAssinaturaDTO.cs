using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    public class PagamentoAssinaturaDTO
    {
        public int? Id { get; set; }

        [Required]
        public int PrestadorId { get; set; }

        [Required]
        public int PlanoAssinaturaId { get; set; }

        [Required]
        public int CartaoId { get; set; }

        [Required]
        public double Valor { get; set; }

        [Required]
        public DateTime DataPagamento { get; set; }
    }
}
