using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    public class AgendamentoDTO
    {
        public int? Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int ServicoId { get; set; }

        [Required]
        public DateTime DataHora { get; set; }

        [Required]
        public string Status { get; set; } = "Pendente";
    }
}
