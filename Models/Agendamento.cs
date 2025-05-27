using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConectaServApi.Models
{
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; } = null!;

        [Required]
        public int ServicoId { get; set; }

        [ForeignKey("ServicoId")]
        public Servico Servico { get; set; } = null!;

        [Required]
        public DateTime DataHora { get; set; }

        [Required]
        public string Status { get; set; } = "Pendente";
    }
}
