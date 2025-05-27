using System.ComponentModel.DataAnnotations;

namespace ConectaServApi.DTOs
{
    public class AgendamentoStatusDTO
    {
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
